using System;
using Overlayer;
using Overlayer.Core.Patches;
using UnityEngine;

namespace MoreTimeTags {
    [LazyPatch("MoreTimeTags.UpdateMusicTime", "scrController", "PlayerControl_Update", Triggers = new[] { "CurMinuteWithPitch", "CurSecondWithPitch", "CurMilliSecondWithPitch", "TotalMinuteWithPitch", "TotalSecondWithPitch", "TotalMilliSecondWithPitch", "LeftMinute", "LeftSecond", "LeftMilliSecond", "LeftMinuteWithPitch", "LeftSecondWithPitch", "LeftMilliSecondWithPitch", "FinishYear", "FinishMonth", "FinishDay", "FinishHour", "FinishMinute", "FinishSecond", "FinishMilliSecond", "MusicProgress" })]
    public class MusicTimePatch {
        public static void Prefix(scrController __instance) {
            if(__instance.paused || !scrConductor.instance.isGameWorld) return;
            AudioSource song = scrConductor.instance.song;
            if(!(bool) song.clip) return;
            TimeSpan curTimeSpanWithPitch = TimeSpan.FromSeconds((double)song.time / song.pitch);
            CustomTags.CurMinuteWithPitch = curTimeSpanWithPitch.Minutes;
            CustomTags.CurSecondWithPitch = curTimeSpanWithPitch.Seconds;
            CustomTags.CurMilliSecondWithPitch = curTimeSpanWithPitch.Milliseconds;
            TimeSpan totalTimeSpanWithPitch = TimeSpan.FromSeconds((double)song.clip.length / song.pitch);
            CustomTags.TotalMinuteWithPitch = totalTimeSpanWithPitch.Minutes;
            CustomTags.TotalSecondWithPitch = totalTimeSpanWithPitch.Seconds;
            CustomTags.TotalMilliSecondWithPitch = totalTimeSpanWithPitch.Milliseconds;
            double leftTime = song.clip.length - song.time;
            TimeSpan leftTimeSpan = TimeSpan.FromSeconds(leftTime);
            CustomTags.LeftMinute = leftTimeSpan.Minutes;
            CustomTags.LeftSecond = leftTimeSpan.Seconds;
            CustomTags.LeftMilliSecond = leftTimeSpan.Milliseconds;
            double leftTimeWithPitch = leftTime / song.pitch;
            TimeSpan leftTimeSpanWithPitch = TimeSpan.FromSeconds(leftTimeWithPitch);
            CustomTags.LeftMinuteWithPitch = leftTimeSpanWithPitch.Minutes;
            CustomTags.LeftSecondWithPitch = leftTimeSpanWithPitch.Seconds;
            CustomTags.LeftMilliSecondWithPitch = leftTimeSpanWithPitch.Milliseconds;
            DateTime finishTime = new DateTime(FastDateTime.Now.Ticks + (long)(leftTimeWithPitch * 10000000L));
            CustomTags.FinishYear = finishTime.Year;
            CustomTags.FinishMonth = finishTime.Month;
            CustomTags.FinishDay = finishTime.Day;
            CustomTags.FinishHour = finishTime.Hour;
            CustomTags.FinishMinute = finishTime.Minute;
            CustomTags.FinishSecond = finishTime.Second;
            CustomTags.FinishMilliSecond = finishTime.Millisecond;
            Main.Instance.Log((double) song.time / song.clip.length);
            CustomTags.MusicProgress = (double) song.time / song.clip.length * 100;
        }
    }
}