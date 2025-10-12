using ClientApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Viewmodels
{
    public partial class AddHouseholdViewmodel : ObservableObject
    {
        [ObservableProperty] Household household = new();
        [ObservableProperty] ObservableCollection<HousingType> housingTypes = new(Enum.GetValues<HousingType>());
        [ObservableProperty] ObservableCollection<BuildingMaterial> buildingMaterials =  new(Enum.GetValues<BuildingMaterial>());
        [ObservableProperty] ObservableCollection<EmploymentStatus> employmentStatuses = new(Enum.GetValues<EmploymentStatus>());

        private int _currentStepIndex;
        public string StepTitle => _stepTitles[_currentStepIndex];
        private readonly string[] _stepTitles = {
            "Household Profile", "Members", "Media", "Services", "Review & Save"
        };

        public bool CanGoBack => _currentStepIndex > 0;
        public bool CanGoNext => _currentStepIndex < 4;
        public int Step { get; set; }
        public bool IsStep1 => Step == 0;
        public bool IsStep2 => Step == 1;
        public bool IsStep3 => Step == 2;
        public bool IsStep4 => Step == 3;
        public bool IsStep5 => Step == 4;

        public event Func<int, int, Task>? OnStepChanged;
        // ===========================
        // 🔹 FORM FIELDS
        // ===========================
        [ObservableProperty] private string? headOfHouseholdName;
        [ObservableProperty] private string? headOfHouseholdIdNumber;
        [ObservableProperty] private string? phoneNumber;
        [ObservableProperty] private string? address;

        // ===========================
        // 🔹 MEMBERS
        // ===========================
        public ObservableCollection<HouseholdMemberVm> Members { get; set; } = new();

        // ===========================
        // 🔹 MEDIA
        // ===========================
        [ObservableProperty] private ImageSource? capturedImage;
        public bool HasImage => CapturedImage != null;
        // ===========================
        // 🔹 SERVICES
        // ===========================
        [ObservableProperty] private bool hasElectricity;
        [ObservableProperty] private bool hasWater;
        [ObservableProperty] private bool hasInternet;
        [ObservableProperty] private bool hasSanitation;

        public AddHouseholdViewmodel()
        {
            
        }

        [RelayCommand]
        public async Task AddMember()
        {
            string name = await Application.Current.MainPage.DisplayPromptAsync("New Member", "Enter member name:");
            if (string.IsNullOrWhiteSpace(name)) return;

            Household.HouseholdMembers ??= new List<HouseholdMember>();
            Household.HouseholdMembers.Add(new HouseholdMember
            {
                FullName = name,
                RelationshipToHead = RelationshipToHead.Other,
                Gender = Gender.Male,
                Household = Household
            });

            OnPropertyChanged(nameof(Household));
        }

        [RelayCommand]
        public async Task CapturePhoto()
        {
            var result = await MediaPicker.Default.CapturePhotoAsync();
            if (result != null)
            {
                var file = await result.OpenReadAsync();
                var fileName = Path.GetFileName(result.FullPath);

                Household.HouseholdMedias ??= new List<HouseholdMedia>();
                Household.HouseholdMedias.Add(new HouseholdMedia
                {
                    FileName = fileName,
                    FilePath = result.FullPath,
                    FileType = FileType.Other
                });

                OnPropertyChanged(nameof(Household));
            }
        }
        // ===========================
        // 🔹 STEP CONTROL LOGIC
        // ===========================
        [RelayCommand]
        public async Task Next()
        {
            if (Step >= 4) return;
            int old = Step;
            Step++;
            OnPropertyChanged(nameof(IsStep1));
            OnPropertyChanged(nameof(IsStep2));
            OnPropertyChanged(nameof(IsStep3));
            OnPropertyChanged(nameof(IsStep4));
            OnPropertyChanged(nameof(IsStep5));
            OnPropertyChanged(nameof(CanGoBack));
            if (OnStepChanged != null)
                await OnStepChanged.Invoke(old, Step);
        }

        [RelayCommand]
        public async Task Previous()
        {
            if (Step <= 0) return;
            int old = Step;
            Step--;
            OnPropertyChanged(nameof(IsStep1));
            OnPropertyChanged(nameof(IsStep2));
            OnPropertyChanged(nameof(IsStep3));
            OnPropertyChanged(nameof(IsStep4));
            OnPropertyChanged(nameof(IsStep5));
            OnPropertyChanged(nameof(CanGoBack));
            if (OnStepChanged != null)
                await OnStepChanged.Invoke(old, Step);
        }

        // ===========================
        // 🔹 SUBMIT
        // ===========================
        [RelayCommand]
        public async Task Submit()
        {
            // Perform validation or API submission here
            await Shell.Current.DisplayAlert(
                "Saved",
                "Household information successfully submitted.",
                "OK"
            );

            // Reset form or navigate away
            Step = 0;
            Members.Clear();
            CapturedImage = null;
            HeadOfHouseholdName = HeadOfHouseholdIdNumber = PhoneNumber = Address = string.Empty;
            HasElectricity = HasWater = HasInternet = false;
            OnPropertyChanged(nameof(IsStep1));
            OnPropertyChanged(nameof(IsStep2));
            OnPropertyChanged(nameof(IsStep3));
            OnPropertyChanged(nameof(IsStep4));
            OnPropertyChanged(nameof(IsStep5));
        }

    }
    public class HouseholdMemberVm
    {
        public string? Name { get; set; }
        public string? Relationship { get; set; }
    }
}
