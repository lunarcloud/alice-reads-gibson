using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ZoneSection {
    public List<int> Zones = new List<int>();

    public ZoneSection(params int[] zones) {
        Zones = zones.OfType<int>().ToList();
    }
}