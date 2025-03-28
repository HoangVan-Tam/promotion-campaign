using Entities.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Constants
{
    public static partial class GlobalConstants
    {
        public enum ProcessEntryStatus
        {
            Success,
            FieldInvalid,
            Repeated,
            AmountInvalid,
            NotInCampaignPeriod
        }
        public enum TypeSubmitForm
        {
            OnlinePage,
            OnlineCompletionPage
        }
        public const string DefaultDateTimeFormat = "dd MMM yyyy HH:mm:ss";

        public static List<DropDownItemData<string>> EntryValidityOptions = new(){
        new DropDownItemData<string>(){Text = "Select All", Code = ""},
        new DropDownItemData<string>(){Text = "Valid", Code = "1"},
        new DropDownItemData<string>(){Text = "Invalid", Code = "0"},
    };
    }
}
