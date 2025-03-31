using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementApp.MVC.Data;

public class ClassMetadata
{
    [Display(Name = "Lecturer")]
    public int LecturerId { get; set; }

    [Display(Name = "Course")]
    public int CourseId { get; set; }

    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date")]
    [DateGreaterThan("StartDate", ErrorMessage = "End Date must be later than Start Date.")]
    public DateTime EndDate { get; set; }
}

[ModelMetadataType(typeof(ClassMetadata))]
public partial class Class { }

public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var currentValue = (DateTime?)value;
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (property == null)
            return new ValidationResult($"Unknown property: {_comparisonProperty}");

        var comparisonValue = (DateTime?)property.GetValue(validationContext.ObjectInstance);

        if (currentValue.HasValue && comparisonValue.HasValue && currentValue <= comparisonValue)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}