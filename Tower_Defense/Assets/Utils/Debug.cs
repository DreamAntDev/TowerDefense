// Uncomment the lines below to mute (most) log messages.
#if !UNITY_EDITOR
#define NO_DEBUG
#endif

// The DEFAULT_DEBUG flag just sets the use of Unity's Debug class for loggine instead of this one.
// This allows the jump-to-line functionality to work properly in the Unity editor console.
#if UNITY_STANDALONE_WIN && !NO_DEBUG
#define DEFAULT_DEBUG
#endif

#if !DEFAULT_DEBUG
using UnityEngine;
using System;

/// 
/// It overrides UnityEngine.Debug to mute debug messages completely on a platform-specific basis.
/// 
/// Putting this inside of 'Plugins' foloder is ok.
/// 
/// Important:
///     Other preprocessor directives than 'UNITY_EDITOR' does not correctly work.
///
/// 2012.11. @kimsama
/// 2016.05. @neegool
/// 
public static class Debug
{
    public static bool isDebugBuild
    {
        get { return UnityEngine.Debug.isDebugBuild; }
    }

    public static void Log(object message)
    {
#if !NO_DEBUG
        UnityEngine.Debug.Log(message);
#endif
    }

    public static void Log(object message, UnityEngine.Object context)
    {
#if !NO_DEBUG
        UnityEngine.Debug.Log(message, context);
#endif
    }

    public static void LogFormat(string format, params object[] args)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogFormat(format, args);
#endif
    }

    public static void LogFormat(UnityEngine.Object context, string format, params object[] args)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogFormat(context, format, args);
#endif
    }

    public static void LogError(object message)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogError(message);
#endif
    }

    public static void LogError(object message, UnityEngine.Object context)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogError(message, context);
#endif
    }

    public static void LogErrorFormat(string format, params object[] args)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogErrorFormat(format, args);
#endif
    }

    public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogErrorFormat(context, format, args);
#endif
    }

    public static void LogWarning(object message)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogWarning(message.ToString());
#endif
    }

    public static void LogWarning(object message, UnityEngine.Object context)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogWarning(message.ToString(), context);
#endif
    }

    public static void LogWarningFormat(string format, params object[] args)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogWarningFormat(format, args);
#endif
    }

    public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogWarningFormat(context, format, args);
#endif
    }

    public static void LogException(Exception exception)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogException(exception);
#endif
    }

    public static void LogException(Exception exception, UnityEngine.Object context)
    {
#if !NO_DEBUG
        UnityEngine.Debug.LogException(exception, context);
#endif
    }

    public static void DrawLine(Vector3 start, Vector3 end, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
    {
#if !NO_DEBUG
        UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
#endif
    }

    public static void DrawRay(Vector3 start, Vector3 dir, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
    {
#if !NO_DEBUG
        UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);
#endif
    }

    public static void Assert(bool condition)
    {
#if !NO_DEBUG
        UnityEngine.Debug.Assert(condition);
#endif
    }

    public static void Assert(bool condition, object message)
    {
#if !NO_DEBUG
        UnityEngine.Debug.Assert(condition, message);
#endif
    }

    public static void Assert(bool condition, object message, UnityEngine.Object context)
    {
#if !NO_DEBUG
        UnityEngine.Debug.Assert(condition, message, context);
#endif
    }

    public static void Assert(bool condition, UnityEngine.Object context)
    {
#if !NO_DEBUG
        UnityEngine.Debug.Assert(condition, context);
#endif
    }
}
#endif