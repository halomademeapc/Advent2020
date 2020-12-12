using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class Day10_PowerAdapters : IPuzzleResult<long>
    {
        public long GetResult()
        {
            var outputs = Resources.Day10Values.Split(Environment.NewLine).Select(int.Parse);
            var jumps = GetJumps(outputs);

            var res = outputs.OrderBy(o => o).Zip(jumps).ToList();

            return jumps.Count(j => j == 1) * jumps.Count(j => j == 3);
        }

        public static IEnumerable<int> GetJumps(IEnumerable<int> adapterOutputs)
        {
            var adapters = adapterOutputs.Select(o => new JoltageAdapter(o));
            var device = GetDevice(adapters);
            var outlet = new Outlet();

            var allAdapters = adapters.Append(device).Append(outlet).OrderBy(a => a.Output);

            return allAdapters.Select((adapter, index) => index > 0 ? adapter - allAdapters.ElementAt(index - 1) : 0);
        }

        public static Device GetDevice(IEnumerable<JoltageAdapter> adapters) => new Device(adapters.Max(a => a.Output));

        public class JoltageAdapter
        {
            public JoltageAdapter(int output)
            {
                Output = output;
            }

            public int Output { get; private set; }

            public virtual int InputCompensation { get; protected set; } = 3;

            public virtual bool CanAcceptInputFrom(JoltageAdapter adapter) => adapter != this && Output - adapter.Output <= InputCompensation;

            public static int operator -(JoltageAdapter adapter1, JoltageAdapter adapter2) => Math.Abs(adapter1.Output - adapter2.Output);
        }

        public class Device : JoltageAdapter
        {
            public Device(int highestAdapterJoltage) : base(highestAdapterJoltage + 3) { }
        }

        public class Outlet : JoltageAdapter
        {
            public Outlet() : base(0) { }

            public override bool CanAcceptInputFrom(JoltageAdapter adapter) => false;
        }


    }
    public class Day10_AdapterArrangements : IPuzzleResult<long>
    {
        public long GetResult()
        {
            throw new NotImplementedException();
        }
    }
}