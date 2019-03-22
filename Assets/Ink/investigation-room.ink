//INCLUDE Other.ink

-> Intro

// VAR example = "asdf"

== Intro ==
It was a messy situation, and I found myself in the muck up to my waders.

-> InBetween

== InBetween ==
You look around the room.
 + {not Lamp.Blood} [Lamp] -> Lamp
 * [Done] -> DONE

== Lamp ==
It was a lamp.
 * [Notice Blood] -> Blood
 + [Don't notice]
 - -> InBetween

= Blood
There was blood upon it!
-> InBetween