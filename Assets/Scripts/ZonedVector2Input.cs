using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonedVector2Input {
    
    public float MinimumMagnitude = 0.3f;

    private int _ActiveZone = -1;
    public int ActiveZone {
        get => _ActiveZone;
    }

    // Zones start due right, going anticlockwise
    private HashSet<ZoneSection> _Sections = new HashSet<ZoneSection>();
    public HashSet<ZoneSection> Sections {
        get => _Sections;
        set {
            Value = Value; // trigger recalculate
        }
    }

    private HashSet<ZoneSection> _ActiveSections = new HashSet<ZoneSection>();
    public HashSet<ZoneSection> ActiveSections {
        get => _ActiveSections;
    }

    public int Zones = 16;

    private Vector2 _Value = Vector2.zero;
    public Vector2 Value {
        get {
            return _Value;
        }
        set {
            _Value = value;

            // Clear previously active
            _ActiveSections.Clear();

            // Skip if still in the center of the touchpad / stick
            if (value.magnitude < MinimumMagnitude) {
                _ActiveZone = -1;
                return;
            }

            // Update Angle Values
            var angle = Mathf.Atan2(value.y, value.x);
            var positiveOnlyAngle =  angle > 0 ? angle : angle + (2 * Mathf.PI);
            _ActiveZone = Mathf.FloorToInt(
                positiveOnlyAngle / (2 * Mathf.PI / Zones) // slice size
            );

            // Discover which sections are active
            foreach (var section in Sections) {
                if (section.Zones.Contains(_ActiveZone)) {
                    _ActiveSections.Add(section);
                }
            }
        }
    }

}