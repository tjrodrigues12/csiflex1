using CSIFLEX.PartAnalyzer.Entities;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace CSIFLEX.PartAnalyzer.ViewModel
{
    public class AdvancedSearchOptionsViewModel : INotifyPropertyChanged
    {
        private readonly Action<MainViewModel> updateSearchOptions;
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Action<bool> updateIterateOverSearchDateValue;
        private readonly Action<int> updateMinuteWindowValue;
        private readonly Action<int> updateTimeWindowSearchIterations;
        private readonly Action<bool> updateIsIteratingOverPartName;
        private readonly Action<int> updateIterativeSearchPartNameValue;
        private readonly Action<bool> updateIsSplittingHyphens;
        private readonly Action<int> updateHourWindowValue;

        public AdvancedSearchOptionsViewModel(
            SearchOptions searchOptions,
            Action<bool> updateIterateOverSearchDateValue,
            Action<int> updateHourWindowValue,
            Action<int> updateMinuteWindowValue,
            Action<int> updateTimeWindowSearchIterations,
            Action<bool> updateIsIteratingOverPartName,
            Action<int> updateIterativeSearchPartNameValue,
            Action<bool> updateIsSplittingHyphens)
        {
            this.updateIterateOverSearchDateValue = updateIterateOverSearchDateValue;
            this.updateHourWindowValue = updateHourWindowValue;
            this.updateMinuteWindowValue = updateMinuteWindowValue;
            this.updateTimeWindowSearchIterations = updateTimeWindowSearchIterations;
            this.updateIsIteratingOverPartName = updateIsIteratingOverPartName;
            this.updateIterativeSearchPartNameValue = updateIterativeSearchPartNameValue;
            this.updateIsSplittingHyphens = updateIsSplittingHyphens;

            this.WhenAnyValue(x => x.IsIterateSearchOverDate)
                .Subscribe(x => this.updateIterateOverSearchDateValue(x));

            this.WhenAnyValue(x => x.HourWindowValue)
               .Subscribe(x => this.updateHourWindowValue(x));

            this.WhenAnyValue(x => x.MinuteWindowValue)
               .Subscribe(x => this.updateMinuteWindowValue(x));

            this.WhenAnyValue(x => x.TimeWindowSearchIterations)
               .Subscribe(x => this.updateTimeWindowSearchIterations(x));

            this.WhenAnyValue(x => x.IsIteratingOverPartName)
               .Subscribe(x => this.updateIsIteratingOverPartName(x));

            this.WhenAnyValue(x => x.IterativeSearchPartNameValue)
               .Subscribe(x => this.updateIterativeSearchPartNameValue(x));

            this.WhenAnyValue(x => x.IsSplittingHyphens)
               .Subscribe(x => this.updateIsSplittingHyphens(x));

            IsIterateSearchOverDate =  searchOptions.IsIterateSearchOverDate;
            HourWindowValue = searchOptions.HourWindowValue;
            MinuteWindowValue = searchOptions.MinuteWindowValue;
            TimeWindowSearchIterations = searchOptions.TimeWindowSearchIterations;
            IsIteratingOverPartName = searchOptions.IsIteratingOverPartName;
            IterativeSearchPartNameValue = searchOptions.IterativeSearchPartNameValue;
            IsSplittingHyphens = searchOptions.IsSplittingHyphens;
        }

        public bool IsIterateSearchOverDate { get; set; }

        public int HourWindowValue { get; set; }

        public int MinuteWindowValue { get; set; }

        public int TimeWindowSearchIterations { get; set; }

        public bool IsIteratingOverPartName { get; set; }

        public int IterativeSearchPartNameValue { get; set; }

        public bool IsSplittingHyphens { get; set; }

    }
}
