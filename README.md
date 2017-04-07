# El_chapo
Project c#

Reflexion sur les pouvoirs + classement :

FIRST signifie que le sort doit obligatoirement être pris en compte avant une eventuelle attaque de l'opposant
!FIRST signifie que le sort n'a pas besoin d'être pris en compte au début et ne doit pas l'être !
NaN : signifie que le pouvoir en question est à la fois FIRST et à la fois !FIRST, à voir comment on gére ça...

FIRST :

BLOQUE :
L’ordonnateur des pompes funèbres - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque Oui
Jarvan cinquième du nom           - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque Oui
Jeff Radis                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque Oui
Chris Hart                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque Oui

Judy Sunny                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.6 pare pour def + 1 Oui

Dead Poule                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.1 pare pour def + 1 Oui	


!FIRST : 
Judy Sunny                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.4 +5HP Non

Triple Hache                      - A : 0.33 | D : 0.33 | S : 0.33 -> 0.2 inflige 2dmg, perd 1HP Non

Dead Poule                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.1 inflige 3 pv + Attaque soigne 3pv Non
								   -> 0.3 +2HP Non

John Cinéma                       - A : 0.33 | D : 0.33 | S : 0.33 -> 0.2 inflige attaque + 2, perd 1HP Non
Raie Mystérieuse                  - A : 0.33 | D : 0.33 | S : 0.33 -> 0.4 perd 3HP Non

ONESHOT :
Bret Benoit                       - A : 0.33 | D : 0.33 | S : 0.33 -> 0.08 HP adversaire = 0 Non





NaN :

Madusa                            - A : 0.33 | D : 0.33 | S : 0.33 -> 0.4 bloque 4 dmgs et inflige 1HP Oui et non
Raie Mystérieuse                  - A : 0.33 | D : 0.33 | S : 0.33 -> 0.6 inflige attaque + 1, bloque 2dmgs Non et Oui
