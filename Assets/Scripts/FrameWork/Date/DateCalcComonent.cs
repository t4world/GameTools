using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// 时间计算
/// </summary>
public class CalcComponent 
{
//	private static CalcComponent _instance = new CalcComponent();
	
	public static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	private const double cntTime = +8.0;
//	private static readonly TimeZoneInfo _timezoneLocal = TimeZoneInfo.Local;

	//DateTime 本地时间到UNIX时间
	// DateTime -> ulong(UNIX Time)
	public static ulong ChangeUnixTime(DateTime time)
	{
		DateTime changeTime = time.ToUniversalTime();
		System.TimeSpan elapsedTime = changeTime - unixEpoch;
		return (ulong)elapsedTime.TotalMilliseconds;
	}

	// China Local time to Unix
	public static ulong ChangeUnixTimeCHT(DateTime time) {
		DateTime gmt = time.AddHours(-cntTime);
		return ChangeUnixTime(gmt);
	}
	
	// ulong(UNIX Time) -> DateTime
	public static DateTime ChangeDateTime(ulong time)
	{
		return unixEpoch.AddMilliseconds((double)time);
	}
	
	public static DateTime ChangeDateTimeJST(ulong time) {
		DateTime gmt = ChangeDateTime(time);
		DateTime jst = gmt.AddHours(cntTime);
		return jst;
	}
	
	/// <summary>
	/// 通过本地时间获得Utc时间
	/// </summary>
	/// <returns>
	/// The local time.
	/// </returns>
	public static System.DateTime GetLocalTime()
	{
		return System.DateTime.UtcNow;
	}

	//UnixTime
	public static bool IsValidTime(ulong startTime, ulong endTime, ulong checkTime) {
		if(endTime == 0)
		{
			endTime = ulong.MaxValue;
		}
		return (checkTime >= startTime && checkTime <= endTime);
	}

	
}
