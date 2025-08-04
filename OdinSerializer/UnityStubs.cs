// Minimal Unity stubs for standalone compilation
#if STANDALONE

using System;
using System.Collections.Generic;
using System.Reflection;

// Simple logging system for OdinSerializer
namespace StandaloneLogging
{
    public enum LogLevel
    {
        Error = 0,
        Warning = 1,
        Info = 2,
        Debug = 3,
        Verbose = 4
    }

    public static class SimpleLogger
    {
        private static LogLevel _currentLevel = LogLevel.Info;

        public static void SetLogLevel(LogLevel level)
        {
            _currentLevel = level;
        }

        public static void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public static void Info(string message)
        {
            Log(LogLevel.Info, message);
        }

        public static void Warning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        public static void Error(string message)
        {
            Log(LogLevel.Error, message);
        }

        private static void Log(LogLevel level, string message)
        {
            if (level <= _currentLevel)
            {
                var prefix = level switch
                {
                    LogLevel.Error => "[ERROR] ",
                    LogLevel.Warning => "[WARN] ",
                    LogLevel.Info => "",
                    LogLevel.Debug => "[DEBUG] ",
                    LogLevel.Verbose => "[VERBOSE] ",
                    _ => ""
                };

                Console.WriteLine($"{prefix}{message}");
            }
        }
    }
}

// UnityEngine stubs
namespace UnityEngine
{
    public static class Debug
    {
        public static void Log(object message) { StandaloneLogging.SimpleLogger.Debug(message?.ToString() ?? "null"); }
        public static void LogWarning(object message) { StandaloneLogging.SimpleLogger.Warning(message?.ToString() ?? "null"); }
        public static void LogError(object message) { StandaloneLogging.SimpleLogger.Error(message?.ToString() ?? "null"); }
        public static void LogException(Exception exception) { StandaloneLogging.SimpleLogger.Error($"Exception: {exception}"); }
    }

    public struct Vector2
    {
        public float x, y;
        public Vector2(float x, float y) { this.x = x; this.y = y; }
    }

    public struct Vector3
    {
        public float x, y, z;
        public Vector3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
    }

    public struct Vector4
    {
        public float x, y, z, w;
        public Vector4(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
    }

    public struct Quaternion
    {
        public float x, y, z, w;
        public Quaternion(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
    }

    public class Application
    {
        public static bool isPlaying => false;
        public static string unityVersion => "Standalone";
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class SerializeFieldAttribute : Attribute 
    { 
        public SerializeFieldAttribute() { }
    }
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class HideInInspectorAttribute : Attribute { }

    public class RuntimeInitializeOnLoadMethodAttribute : Attribute
    {
        public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType) { }
    }

    public enum RuntimeInitializeLoadType
    {
        AfterSceneLoad,
        BeforeSceneLoad,
        AfterAssembliesLoaded,
        BeforeSplashScreen,
        SubsystemRegistration
    }

    public static class PlayerPrefs
    {
        public static string GetString(string key, string defaultValue = "") => defaultValue;
        public static void SetString(string key, string value) { }
    }

    public abstract class Object
    {
        public static bool operator ==(Object x, Object y) => ReferenceEquals(x, y);
        public static bool operator !=(Object x, Object y) => !ReferenceEquals(x, y);
        public override bool Equals(object obj) => ReferenceEquals(this, obj);
        public override int GetHashCode() => base.GetHashCode();
    }

    /// <summary>
    /// Interface for objects that need custom serialization callback behavior
    /// </summary>
    public interface ISerializationCallbackReceiver
    {
        void OnBeforeSerialize();
        void OnAfterDeserialize();
    }

    public enum RuntimePlatform
    {
        OSXEditor = 0,
        OSXPlayer = 1,  
        WindowsPlayer = 2,
        OSXWebPlayer = 3,
        OSXDashboardPlayer = 4,
        WindowsWebPlayer = 5,
        WindowsEditor = 7,
        IPhonePlayer = 8,
        XBOX360 = 10,
        PS3 = 9,
        Android = 11,
        NaCl = 12,
        FlashPlayer = 15,
        LinuxPlayer = 13,
        LinuxEditor = 16,
        WebGLPlayer = 17,
        MetroPlayerX86 = 18,
        WSAPlayerX86 = 18,
        MetroPlayerX64 = 19,
        WSAPlayerX64 = 19,
        MetroPlayerARM = 20,
        WSAPlayerARM = 20,
        WP8Player = 21,
        BB10Player = 22,
        BlackBerryPlayer = 22,
        TizenPlayer = 23,
        PSP2 = 24,
        PS4 = 25,
        PSM = 26,
        XboxOne = 27,
        SamsungTVPlayer = 28,
        WiiU = 30,
        tvOS = 31,
        Switch = 32,
        Lumin = 33
    }
    
    namespace Serialization
    {
        public class FormerlySerializedAsAttribute : Attribute
        {
            public string oldName;
            public FormerlySerializedAsAttribute(string oldName) { this.oldName = oldName; }
        }
    }
}

// Unity Editor stubs
namespace UnityEditor
{
    public static class EditorApplication
    {
        public static bool isPlaying => false;
    }
}

// JetBrains stubs
namespace JetBrains.Annotations
{
    public class NotNullAttribute : Attribute { }
    public class CanBeNullAttribute : Attribute { }
    public class MeansImplicitUseAttribute : Attribute { }
}

// System.Reflection.Emit stubs for compatibility
namespace System.Reflection.Emit
{
    public static class Flags
    {
        public const MethodAttributes Public = MethodAttributes.Public;
        public const MethodAttributes Static = MethodAttributes.Static;
        public const MethodAttributes Virtual = MethodAttributes.Virtual;
        public const MethodAttributes HideBySig = MethodAttributes.HideBySig;
        public const MethodAttributes NewSlot = MethodAttributes.NewSlot;
        public const MethodAttributes SpecialName = MethodAttributes.SpecialName;
        public const MethodAttributes RTSpecialName = MethodAttributes.RTSpecialName;
    }
}

// OdinSerializer specific stubs
namespace OdinSerializer
{
    public class UnityReferenceResolver : IExternalIndexReferenceResolver
    {
        public UnityReferenceResolver() { }
        
        public bool CanReference(object value, out int index)
        {
            index = -1;
            return false;
        }
        
        public bool TryResolveReference(int index, out object result)
        {
            result = null;
            return false;
        }
        
        public List<UnityEngine.Object> GetReferencedUnityObjects() => new List<UnityEngine.Object>();
        public void SetReferencedUnityObjects(List<UnityEngine.Object> objects) { }
    }
    
    public static class EmitUtilities
    {
        public static bool CanEmit => false;
        public static void CreateStaticReferenceFor(Type type, out FieldInfo field)
        {
            field = null;
        }
    }
    
    public static class FormatterEmitter
    {
        public static bool CanEmit => false;
        public static string PRE_EMITTED_ASSEMBLY_NAME => "OdinSerializer.EmittedFormatters";
        public static IFormatter GetEmittedFormatter(Type type) => null;
        public static IFormatter GetEmittedFormatter(Type type, ISerializationPolicy policy) => null;
    }
    
    public static class UnitySerializationUtility
    {
        public static bool GuessIfUnityWillSerialize(FieldInfo field) 
        {
            // Check if the field has SerializeField attribute
            if (field.GetCustomAttribute<UnityEngine.SerializeFieldAttribute>() != null)
                return true;
                
            // Public fields are serialized by default in Unity
            if (field.IsPublic && !field.IsStatic && !field.IsInitOnly)
                return true;
                
            return false;
        }
        
        public static Type SerializeReferenceAttributeType => null;
    }
    
    public class DelegateFormatter<T> : IFormatter<T>
    {
        public Type SerializedType => typeof(T);
        public bool CanRead => false;
        public bool CanWrite => false;
        public T Deserialize(IDataReader reader) => default(T);
        object IFormatter.Deserialize(IDataReader reader) => Deserialize(reader);
        public void Serialize(T value, IDataWriter writer) { }
        public void Serialize(object value, IDataWriter writer) => Serialize((T)value, writer);
    }
    
    public class WeakDelegateFormatter : IFormatter
    {
        public WeakDelegateFormatter() { }
        public WeakDelegateFormatter(object delegateRef) { }
        
        public Type SerializedType => typeof(object);
        public bool CanRead => false;
        public bool CanWrite => false;
        public object Deserialize(IDataReader reader) => null;
        public void Serialize(object value, IDataWriter writer) { }
    }
}

#endif