using System.Collections.Generic;

namespace Events.IO.Application.ViewModels
{
    public class ProvinceViewModel
    {
        public string PA { get; set; }
        public string Name { get; set; }

        public static List<ProvinceViewModel> GetProvincesList()
        {
            return new List<ProvinceViewModel>()
            {
                new ProvinceViewModel() {PA="ON", Name = "Ontario"},
                new ProvinceViewModel() {PA="QC", Name = "Quebec"},
                new ProvinceViewModel() {PA="NS", Name = "Nova Scotia"},
                new ProvinceViewModel() {PA="NB", Name = "New Brunswick"},
                new ProvinceViewModel() {PA="MB", Name = "Manitoba"},
                new ProvinceViewModel() {PA="BC", Name = "British Columbia"},
                new ProvinceViewModel() {PA="PE", Name = "Prince Edward Island"},
                new ProvinceViewModel() {PA="SK", Name = "Saskatchewan"},
                new ProvinceViewModel() {PA="AB", Name = "Alberta"},
                new ProvinceViewModel() {PA="NL", Name = "Newfoundland and Labrador"}
            };
        }
    }
}
