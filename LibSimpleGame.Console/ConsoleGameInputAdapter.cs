using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSimpleGame.Console
{
    public class ConsoleGameInputAdapter : GameInputAdapter
    {
        private bool destroy = false;

        private readonly ConcurrentDictionary<ConsoleKey, string> key2buttonMap =
            new ConcurrentDictionary<ConsoleKey, string>();
        private readonly ConcurrentDictionary<ConsoleKey, string> key2axisLowMap =
            new ConcurrentDictionary<ConsoleKey, string>();
        private readonly ConcurrentDictionary<ConsoleKey, string> key2axisHighMap =
            new ConcurrentDictionary<ConsoleKey, string>();

        private readonly ConcurrentDictionary<string, bool> buttonRawStates =
            new ConcurrentDictionary<string, bool>();
        private readonly ConcurrentDictionary<string, bool> buttonLastStates =
            new ConcurrentDictionary<string, bool>();
        private readonly ConcurrentDictionary<string, bool> buttonStates =
            new ConcurrentDictionary<string, bool>();

        private readonly ConcurrentDictionary<string, float> axisRawValues =
            new ConcurrentDictionary<string, float>();
        private readonly ConcurrentDictionary<string, float> axisValues =
            new ConcurrentDictionary<string, float>();
        private readonly ConcurrentDictionary<string, float> axisVelocities =
            new ConcurrentDictionary<string, float>();

        private Point currentCursorPosition;

        private void ProcessKeyEvent(KeyEvent keyEvent)
        {
            if (key2buttonMap.TryGetValue(keyEvent.Key, out string? buttonName))
            {
                buttonRawStates[buttonName] = keyEvent.Pressed;
            }

            if (key2axisLowMap.TryGetValue(keyEvent.Key, out string? axisLowName))
            {
                if (keyEvent.Pressed)
                    axisRawValues[axisLowName] = -1;
                else
                    axisRawValues[axisLowName] = 0;
            }

            if (key2axisHighMap.TryGetValue(keyEvent.Key, out string? axisHighName))
            {
                if (keyEvent.Pressed)
                    axisRawValues[axisHighName] = 1;
                else
                    axisRawValues[axisHighName] = 0;
            }

            Debug.WriteLine($"KeyEvent, Key: {keyEvent.Key}, Pressed: {keyEvent.Pressed}");
        }

        public ConsoleGameInputAdapter()
        {
            NativeApi.EnableMouseInput();
        }

        public override void Start()
        {
            Task.Run(() =>
            {
                try
                {
                    uint bufferSize = 128;
                    var buffer = new NativeApi.INPUT_RECORD[bufferSize];
                    while (true)
                    {
                        NativeApi.ReadConsoleInput(NativeApi.GetStdHandle(-10), buffer, bufferSize, out uint numRead);

                        if (destroy)
                            break;

                        for (uint i = 0; i < numRead; i++)
                        {
                            var input = buffer[i];
                            switch (input.EventType)
                            {
                                case NativeApi.InputRecordEventType.KEY_EVENT:
                                {
                                    var keybd = input.KeyEvent;
                                    var keyEvent = new KeyEvent(keybd.dwControlKeyState, keybd.bKeyDown, (ConsoleKey)keybd.wVirtualKeyCode, keybd.UnicodeChar);
                                    ProcessKeyEvent(keyEvent);
                                    break;
                                }
                                case NativeApi.InputRecordEventType.MOUSE_EVENT:
                                {
                                    var mouse = input.MouseEvent;
                                    if (mouse.dwEventFlags == NativeApi.MouseEventFlags.MOUSE_MOVED)
                                    {
                                        currentCursorPosition = new Point(mouse.dwMousePosition.X, mouse.dwMousePosition.Y);
                                        Debug.WriteLine($"Mouse moved: {mouse.dwMousePosition.X}, {mouse.dwMousePosition.Y}");
                                    }

                                    break;
                                }
                                case NativeApi.InputRecordEventType.WINDOW_BUFFER_SIZE_EVENT:
                                case NativeApi.InputRecordEventType.FOCUS_EVENT:
                                default:
                                    break;
                            }
                        }
                    }
                }
                catch
                {
                    Debugger.Break();
                }
            });
        }

        public override void Update()
        {
            if (Input == null)
                return;

            foreach (var buttonName in buttonRawStates.Keys)
            {
                buttonLastStates[buttonName] = buttonStates[buttonName];
                buttonStates[buttonName] = buttonRawStates[buttonName];
            }

            foreach (var axisName in axisRawValues.Keys)
            {
                float velocity = axisVelocities[axisName];
                float current = axisValues[axisName];
                float target = axisRawValues[axisName];
                float newValue = MathUtils.SmoothDamp(current, target, ref velocity, .1f, float.PositiveInfinity, (float)Input.Game.Time.Delta.TotalSeconds);

                axisVelocities[axisName] = velocity;
                axisValues[axisName] = newValue;
            }
        }
        public override void Stop()
        {
            destroy = true;
        }

        public override float GetAxis(string axisName)
        {
            if (axisValues.TryGetValue(axisName, out var value))
                return value;

            return 0;
        }
        public override float GetAxisRaw(string axisName)
        {
            if (axisRawValues.TryGetValue(axisName, out var value))
                return value;

            return 0;
        }

        public override bool GetButton(string buttonName)
        {
            if (buttonStates.TryGetValue(buttonName, out bool value))
                return value;

            return false;
        }

        public override bool GetButtonDown(string buttonName)
        {
            if (buttonStates.TryGetValue(buttonName, out bool value))
                if (buttonLastStates.TryGetValue(buttonName, out bool lastValue))
                    return value && !lastValue;

            return false;
        }

        public override bool GetButtonUp(string buttonName)
        {
            if (buttonStates.TryGetValue(buttonName, out bool value))
                if (buttonLastStates.TryGetValue(buttonName, out bool lastValue))
                    return !value && lastValue;

            return false;
        }

        public override Point GetCursorPosition() => currentCursorPosition;

        public void RegisterButton(string buttonName, ConsoleKey key)
        {
            buttonRawStates[buttonName] = false;
            buttonLastStates[buttonName] = false;
            buttonStates[buttonName] = false;
            key2buttonMap[key] = buttonName;
        }

        public void RegisterAxis(string axisName, ConsoleKey lowKey, ConsoleKey highKey)
        {
            axisRawValues[axisName] = 0;
            axisValues[axisName] = 0;
            axisVelocities[axisName] = 0;
            key2axisLowMap[lowKey] = axisName;
            key2axisHighMap[highKey] = axisName;
        }
    }
}
