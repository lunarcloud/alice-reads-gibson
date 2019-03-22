//INCLUDE Other.ink

-> Intro

// VAR example = "asdf"

== Intro ==
It was a messy situation, and I found myself in the muck up to my waders.

-> InBetween

== InBetween ==
You look around the room.
 + {not Lamp.Blood} [Lamp] -> Lamp
 + [Talk to the inspector] -> Inspector
 * [Done] -> DONE

== Lamp ==
It was a lamp.
 * [Notice Blood] -> Blood
 + [Don't notice]
 - -> InBetween

= Blood
Where'd this come from?
-> InBetween

== Inspector ==
What do you want? # inspector
He was a pushy fella.
{Lamp.Blood:Did you see the bloodstain?|Nothing.}  #you
-> InBetween
