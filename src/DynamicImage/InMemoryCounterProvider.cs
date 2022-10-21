using System;
using System.Collections.Generic;

namespace DynamicImage
{
    public class InMemoryCounterProvider : ICounterProvider
    {
        private readonly Dictionary<Guid, int> counters = new();

        public int Increment(Guid counterId)
        {
            if (!counters.ContainsKey(counterId))
            {
                counters.Add(counterId, 0);
            }

            int currentCounter = counters[counterId];
            counters[counterId] = currentCounter + 1;
            return currentCounter;
        }
    }
}