using System.ComponentModel.DataAnnotations;

namespace Web_253502_Alkhovik.Blazor.SSR.Models;

public class CounterModel
{
    [Range(1,10)]
    public int Value { get; set; }
}