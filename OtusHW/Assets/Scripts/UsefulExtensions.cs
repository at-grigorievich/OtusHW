using System;
using UnityEngine;

public static class UsefulExtensions
{
    public static TimeSpan ToTimeSpan(this Vector3Int cooldown) =>
        new(cooldown.x, cooldown.y, cooldown.z);

    public static string ToStringFormat(this TimeSpan timeSpan)
    {
        //Can add localization
        
        //return $"{timeSpan.Hours}h.:{timeSpan.Minutes}m.:{timeSpan.Seconds}s.";
        return $"{timeSpan.Hours}ч.:{timeSpan.Minutes}м.:{timeSpan.Seconds}с.";
    }
}