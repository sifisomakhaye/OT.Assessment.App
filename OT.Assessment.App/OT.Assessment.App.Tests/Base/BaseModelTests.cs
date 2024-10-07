using System;
using Xunit;

namespace OT.Assessment.App.Tests
{
    public abstract class BaseModelTests<T> where T : new()
    {
        protected T CreateDefaultModel()
        {
            return new T();
        }

        protected void AssertModelProperties(T model, Action<T> assert)
        {
            assert(model);
        }
    }
}