using System;

namespace Directum.Calendar
{
    public static class MeetingTools
    {
        public static bool Intercept(MeetingModel model1, MeetingModel model2)
        {
            if (model2.StartDateTime >= model1.StartDateTime && model2.StartDateTime < model1.EndDateTime) return true;
            if (model2.EndDateTime > model1.StartDateTime && model2.EndDateTime < model1.EndDateTime) return true;
            if (model1.StartDateTime >= model2.StartDateTime && model1.StartDateTime < model2.EndDateTime) return true;
            if (model1.EndDateTime > model2.StartDateTime && model1.EndDateTime < model2.EndDateTime) return true;
            return false;
        }
    }
}