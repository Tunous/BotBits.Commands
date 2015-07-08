﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BotBits.Commands.Source;

namespace BotBits.Commands
{
    public sealed class Command
    {
        public Command(string[] names, string[] usages, int minArgs, Action<IInvokeSource, ParsedCommand> callback)
        {
            this.Names = names;
            this.Usages = usages;
            this.MinArgs = minArgs;
            this.Callback = callback;
        }

        internal Command(Action<IInvokeSource, ParsedCommand> callback)
        {
            this.Callback = callback;

            var method = callback.Method;
            var command = (CommandAttribute)method.GetCustomAttributes(typeof(CommandAttribute), false).FirstOrDefault();
            if (command == null) throw new ArgumentException("The given callback is not a command", "callback");

            this.Names = command.Names;
            this.Usages = command.Usages;
            this.MinArgs = command.MinArgs;
        }

        public string[] Names { get; private set; }

        public string[] Usages { get; private set; }

        public int MinArgs { get; private set; }

        public Action<IInvokeSource, ParsedCommand> Callback { get; private set; }
    }
}