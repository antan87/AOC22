namespace Day3;
internal record Rucksack(List<Compartment> Compartments)
{
    public string FindDuplicate()
    {
        return Compartments.SelectMany(compartment => compartment.Items.Distinct()).GroupBy(x => x).First(group => group.Count() > 1).First();
    }
}