using System;
using System.Collections.Generic;

public abstract class ClassContainer
{
    protected static IDictionary<Type, IServiceInit> services = new Dictionary<Type, IServiceInit>();
    protected static IDictionary<Type, IServiceUpdate> serviceUpdate = new Dictionary<Type, IServiceUpdate>();

    public static T GetService<T>()
    {
        try
        {
            return (T)services[typeof(T)];
        }
        catch (KeyNotFoundException)
        {
            throw new Exception("Service " + typeof(T) + " is absent");
        }
    }

    public static void AddService<T>(IServiceInit service)
    {
        if (service is T)
        {
            services.Add(typeof(T), service);

        }
        else
        {
            throw new Exception("Service " + service.ToString() + " have not implemented interface: " + typeof(T));
        }
    }

    public static void AddUpdateService<T>(IServiceUpdate service)
    {
        if (service is T)
        {
            serviceUpdate.Add(typeof(T), service);

        }
        else
        {
            throw new Exception("Service Update" + service.ToString() + " have not implemented interface: " + typeof(T));
        }
    }
    public static IDictionary<Type, IServiceInit> GetServiceList()
    {
        return services;
    }


    public static void InitServices()
    {

        foreach (IServiceInit service in services.Values)
            service.Init();
    }


    public static void Update()
    {
        foreach (IServiceUpdate service in serviceUpdate.Values)
            service.Update();
    }


    public static void Dispose()
    {
        foreach (IServiceInit service in services.Values)
            service.Dispose();
    }
}