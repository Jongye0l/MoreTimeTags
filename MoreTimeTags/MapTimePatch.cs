using System;
using System.Collections.Generic;
using Overlayer;
using Overlayer.Core.Patches;

namespace MoreTimeTags {
    [LazyPatch("MoreTimeTags.UpdateMapTime", "scnEditor", "Update", Triggers = new[] { "CurMapMinute", "CurMapSecond", "CurMapMilliSecond", "TotalMapMinute", "TotalMapSecond", "TotalMapMilliSecond", "PitchedCurMapMinute", "PitchedCurMapSecond", "PitchedCurMapMilliSecond", "PitchedTotalMapMinute", "PitchedTotalMapSecond", "PitchedTotalMapMilliSecond", "LeftMapMinute", "LeftMapSecond", "LeftMapMilliSecond", "PitchedLeftMapMinute", "PitchedLeftMapSecond", "PitchedLeftMapMilliSecond", "FinishMapYear", "FinishMapMonth", "FinishMapDay", "FinishMapHour", "FinishMapMinute", "FinishMapSecond", "FinishMapMilliSecond", "TimeProgress" })]
    public class MapTimePatch {
        public static void Postfix() {
            if (!scrConductor.instance.isGameWorld || scnEditor.instance.inStrictlyEditingMode || ADOBase.controller.state == States.Fail) return;
            double pitch = scrConductor.instance.song.pitch;
            double dummyCurTime = scrConductor.instance.songposition_minusi + scrConductor.instance.addoffset;
            double curTime;
            List<scrFloor> floors = scrLevelMaker.instance.listFloors;
            double totalTime = floors[floors.Count - 1].entryTime + scrConductor.instance.addoffset;
            if(dummyCurTime > totalTime) curTime = totalTime;
            else if(dummyCurTime < 0) curTime = 0;
            else curTime = dummyCurTime;
            TimeSpan mapTimeSpan = TimeSpan.FromSeconds(curTime);
            CustomTags.CurMapMinute = mapTimeSpan.Minutes;
            CustomTags.CurMapSecond = mapTimeSpan.Seconds;
            CustomTags.CurMapMilliSecond = mapTimeSpan.Milliseconds;
            TimeSpan mapTimeSpanWithPitch = TimeSpan.FromSeconds(curTime / pitch);
            CustomTags.CurMapMinuteWithPitch = mapTimeSpanWithPitch.Minutes;
            CustomTags.CurMapSecondWithPitch = mapTimeSpanWithPitch.Seconds;
            CustomTags.CurMapMilliSecondWithPitch = mapTimeSpanWithPitch.Milliseconds;
            TimeSpan totalTimeSpan = TimeSpan.FromSeconds(totalTime);
            CustomTags.TotalMapMinute = totalTimeSpan.Minutes;
            CustomTags.TotalMapSecond = totalTimeSpan.Seconds;
            CustomTags.TotalMapMilliSecond = totalTimeSpan.Milliseconds;
            TimeSpan totalTimeSpanWithPitch = TimeSpan.FromSeconds(totalTime / pitch);
            CustomTags.TotalMapMinuteWithPitch = totalTimeSpanWithPitch.Minutes;
            CustomTags.TotalMapSecondWithPitch = totalTimeSpanWithPitch.Seconds;
            CustomTags.TotalMapMilliSecondWithPitch = totalTimeSpanWithPitch.Milliseconds;
            double leftTime = totalTime - curTime;
            TimeSpan leftTimeSpan = TimeSpan.FromSeconds(leftTime);
            CustomTags.LeftMapMinute = leftTimeSpan.Minutes;
            CustomTags.LeftMapSecond = leftTimeSpan.Seconds;
            CustomTags.LeftMapMilliSecond = leftTimeSpan.Milliseconds;
            double leftTimeWithPitch = leftTime / pitch;
            TimeSpan leftTimeSpanWithPitch = TimeSpan.FromSeconds(leftTimeWithPitch);
            CustomTags.LeftMapMinuteWithPitch = leftTimeSpanWithPitch.Minutes;
            CustomTags.LeftMapSecondWithPitch = leftTimeSpanWithPitch.Seconds;
            CustomTags.LeftMapMilliSecondWithPitch = leftTimeSpanWithPitch.Milliseconds;
            DateTime finishTime = new DateTime(FastDateTime.Now.Ticks + (long)((totalTime - dummyCurTime) / pitch * 10000000L));
            CustomTags.FinishMapYear = finishTime.Year;
            CustomTags.FinishMapMonth = finishTime.Month;
            CustomTags.FinishMapDay = finishTime.Day;
            CustomTags.FinishMapHour = finishTime.Hour;
            CustomTags.FinishMapMinute = finishTime.Minute;
            CustomTags.FinishMapSecond = finishTime.Second;
            CustomTags.FinishMapMilliSecond = finishTime.Millisecond;
            CustomTags.TimeProgress = curTime / totalTime * 100;
        }
    }
}