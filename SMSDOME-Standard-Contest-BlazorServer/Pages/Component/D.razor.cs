using Entities.Component;
using Microsoft.AspNetCore.Components;

namespace SMSDOME_Standard_Contest_BlazorServer.Pages.Component
{
    public partial class D<T> where T : class
    {
        [Parameter] public List<DropDownItemData<T>> DataSource { get; set; } = new();
        [Parameter] public List<DropDownItemData<T>> InitSelectedItems { get; set; } = new();
        [Parameter] public bool EnableMultipleSelection { get; set; } = false;
        [Parameter] public bool HasSearch { get; set; } = false;
        [Parameter] public string Value { get; set; } = "";
        [Parameter] public string Label { get; set; } = "Data";
        [Parameter] public EventCallback<List<DropDownItemData<T>>> ValuesChanged { get; set; }
        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        private string Text;
        private bool IsDropdownOpen { get; set; } = false;
        private string Search { get; set; }
        private List<DropDownItemData<T>> FilteredDataSource { get; set; } = new();
        private List<DropDownItemData<T>> SelectedItems { get; set; } = new();

        protected override void OnInitialized()
        {
            if (InitSelectedItems.Any())
            {
                SelectedItems.AddRange(InitSelectedItems);
            }
        }

        protected override void OnParametersSet()
        {
            FilteredDataSource = DataSource;
        }

        private async void ToggleSelection(DropDownItemData<T> item)
        {
            if (EnableMultipleSelection)
            {
                if (SelectedItems.Any(p => p.Id == item.Id))
                {
                    SelectedItems.Remove(item);
                }
                else
                {
                    SelectedItems.Add(item);
                }
                Text = string.Join(",", SelectedItems.Select(p => p.Text));
                Value = string.Join(",", SelectedItems.Select(p => p.Code));
                await ValuesChanged.InvokeAsync(SelectedItems);
            }
            else
            {
                if (SelectedItems.Any())
                {
                    SelectedItems[0] = item;
                }
                else
                {
                    SelectedItems.Add(item);
                }
                Text = item.Text;
                Value = item.Code.ToString();
                await ValueChanged.InvokeAsync(Value);
                IsDropdownOpen = !IsDropdownOpen;
            }
        }

        private void UpdateSearchResults(ChangeEventArgs __e)
        {
            Search = __e?.Value?.ToString();
            if (!string.IsNullOrEmpty(Search))
            {
                FilteredDataSource = DataSource.Where(p => p.Text.Contains(Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                FilteredDataSource = DataSource;
            }
        }

        private string FindItemText(Guid id)
        {
            return DataSource.FirstOrDefault(p => p.Id == id)?.Text ?? "";
        }
        private bool IsItemSelected(DropDownItemData<T> item)
        {
            return SelectedItems.Any(p => p.Id == item.Id);
        }
    }
}
