using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonedVector2Input {
    public Vector2 CenterIgnoreZone = new Vector2(0.3f, 0.3f);

    private float _Angle;
    public float Angle {
        get => _Angle;
    }
    private float _PositiveOnlyAngle;
    public float PositiveOnlyAngle {
        get => _PositiveOnlyAngle;
    }
    
    private int _ActiveZone;
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

            // Update Angle Values
            _Angle = Mathf.Atan2(value.y, value.x);
            _PositiveOnlyAngle =  _Angle > 0 ? _Angle : _Angle + (2 * Mathf.PI);
            var sliceSize = 2 * Mathf.PI / Zones;
            _ActiveZone = Mathf.FloorToInt(_PositiveOnlyAngle / sliceSize);

            // Discover which sections are active
            _ActiveSections.Clear();
            foreach (var section in Sections) {
                if (section.Zones.Contains(_ActiveZone)) {
                    _ActiveSections.Add(section);
                }
            }
        }
    }

}