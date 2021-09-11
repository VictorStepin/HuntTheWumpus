﻿Hunt the Wumpus!

[ ][ ][ ][ ][ ][ ]
[ ][ ][B][ ][W][ ]
[ ][ ][ ][ ][ ][ ]
[ ][@][ ][O][ ][ ]
[ ][ ][ ][ ][B][ ]
[ ][O][ ][ ][ ][ ]

@ - Игрок (1 клетка)
W - Вампус (1 клетка)
B - Летучие мыши (2 клетки)
O - Яма (2 клетки)

ПРАВИЛА

Подготовка игры:
- Игрок спавнится на случайной клетке
- Вампус спавнится на случайной клетке, кроме клетки Игрока и не в радиусе одной клетки от Игрока
- Летучие мыши спавнятся на случайных клетках, кроме клеток с Игроком, Вампусом и не в радиусе одной клетки от Игрока
- Ямы спавнятся на случайных клетках, кроме клеток с Игроком, Вампусом, летучими мышами и не в радиусе одной клетки от Игрока

Основные моменты:
- Игрок перемещается по полю вверх, вниз, вправо, влево (по диагонали не может) с помощью клавиш WASD
- У Игрока есть меч, которым он может наносить удар на одну клетку вверх, вниз, вправо, влево (по диагонали не может), нажимая на клавиши со стрелками
- Если Игрок при ударе попадает в клетку с Вампусом, то Игрок побеждает (Игрок убивает Вампуса)
- Если Игрок и Вампус оказываются на одной клетке, то игра окончена (Вампус съел Игрока)
- Вампус перемещается на соседнюю клетку с определенной вероятностью после действия Игрока (перемещения или удара)
- Если Игрок попадает на клетку с ямой, то игра проиграна (Игрок упал в смертельную яму)
- Если Игрок попадает на клетку с летучими мышами, то сразу перемещается на случайную клетку на поле (летучие мыши переносят Игрока)
- Клетки с ямой и летучими мышами не влияют на Вампуса (присоски на конечностях позволяют Вампусу ползти по стенкам ямы, а большая масса не позволяет летучим мышам переносить Вампуса)
- Изначально Игрок не видит расположение непустых клеток. 
- Если в радиусе одной клетки от Игрока находится непустая клекта, то выводится сообщение сигнализирующее о нахождении той или иной клетки рядом
- Сообщения:
	- Вы ощущаете отвратительный запах (в радиусе одной клетки находится Вампус)
	- Вы слышыте мерзкий шум (в радиусе одной клетки находится рой летучих мышей)
	- Вы чувствуете сквозняк (в радиусе одной клетки находится яма)
- Содержимое непустой клетки раскрывается, когда игрок попадает на соответствующую клетку (после попадания на клетку с Роем, расположение роя становится известно)