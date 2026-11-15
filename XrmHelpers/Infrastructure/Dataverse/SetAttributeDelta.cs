using System;
using Microsoft.Xrm.Sdk;

namespace custos.XrmHelpers.Infrastructure.Dataverse
{
    public class SetAttributeDelta
    {
        public static void Execute<T>(Entity target, string attributeName, T value)
        {
            try
            {
                target[attributeName] = value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in SetAttributeDelta: {0}", ex);
                throw;
            }
        }
        public static void Execute<T>(Entity target, string attributeName, T newValue, T oldValue)
        {
            try
            {
                if (!EqualityComparer<T>.Default.Equals(newValue, oldValue))
                {
                    target[attributeName] = newValue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in SetAttributeDelta: {0}", ex);
                throw;
            }
        }

        public static void Execute(Entity target, string attributeName, OptionSetValue newValue, OptionSetValue oldValue)
        {
            try
            {
                if (oldValue == null || newValue == null || oldValue.Value != newValue.Value)
                {
                    target[attributeName] = newValue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in SetDeltaOptionSet: {0}", ex);
                throw;
            }
        }

        public static void Execute(Entity target, string attributeName, EntityReference newValue, EntityReference oldValue)
        {
            try
            {
                if (oldValue == null || newValue == null || oldValue.Id != newValue.Id || oldValue.LogicalName != newValue.LogicalName)
                {
                    target[attributeName] = newValue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in SetDeltaLookup: {0}", ex);
                throw;
            }
        }

        public static void Execute(Entity target, string attributeName, DateTime? newValue, DateTime? oldValue)
        {
            try
            {
                DateTime? newUtcDateTime = newValue?.ToUniversalTime();
                DateTime? oldUtcDateTime = oldValue?.ToUniversalTime();

                if (newUtcDateTime != oldUtcDateTime)
                {
                    target[attributeName] = newValue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in SetDeltaDateTime: {0}", ex);
                throw;
            }
        }
    }
}