using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace GPAA.WebBase.UnityConfiguration
{
    //using Base;

    /// <summary>
    /// Caching of the registered types in unity
    /// </summary>
    public static class UnityRegistrationCache
    {
        #region Private

        private static readonly object LockObject = new object();
        private static HashSet<Type> registrations;

        /// <summary>
        /// Register types
        /// </summary>
        private static void Register(IUnityContainer container)
        {
            lock (LockObject)
            {
                if (registrations != null)
                {
                    return;
                }
                registrations = new HashSet<Type>();
                container.Registrations.Where(r => r.Name == null).ForEach(r =>
                {
                    if (!registrations.Contains(r.RegisteredType))
                    {
                        registrations.Add(r.RegisteredType);
                    }
                });
            }
        }

        #endregion

        /// <summary>
        /// True if the type is registered
        /// </summary>
        public static bool IsRegistered(IUnityContainer container, Type typeToCheck)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (typeToCheck == null)
            {
                throw new ArgumentNullException("typeToCheck");
            }

            if (registrations == null)
            {
                Register(container);
            }

            // ReSharper disable PossibleNullReferenceException
            return registrations.Contains(typeToCheck);
            // ReSharper restore PossibleNullReferenceException
        }
    }
}
