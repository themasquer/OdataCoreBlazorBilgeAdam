using Microsoft.JSInterop;

namespace BlazorWasmUI.Helpers.Classes
{
    public static class BlazorBootstrapDatePickerHelper
    {
        private static string _date;
        private static string _startDate;
        private static string _endDate;

        [JSInvokable]
        public static void SetBlazorDate(string date)
        {
            _date = date;
        }

        [JSInvokable]
        public static void SetBlazorStartDate(string startDate)
        {
            _startDate = startDate;
        }

        [JSInvokable]
        public static void SetBlazorEndDate(string endDate)
        {
            _endDate = endDate;
        }

        public static string GetBlazorDate()
        {
            return _date;
        }

        public static string GetBlazorStartDate()
        {
            return _startDate;
        }

        public static string GetBlazorEndDate()
        {
            return _endDate;
        }
    }
}
