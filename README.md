| [Game rules](#Game-rules) | [TODO](#TODO) | [Refactoring](#Refactoring) |[Naming](#Naming) | [My codestyle](#My-codestyle) |
|---------------------------|---------------|-----------------------------|------------------|-------------------------------|

# Lines98

Unity version - 2019.4.17f1. Choose older version for this project because is't faster and easier to prototype.



## Game rules
[Play Online (version with small orbs)](https://www.min2win.ru/gms/834.html)

An old game about collecting orbs in a line. 
- When assembling more than 5 orbs in a line of the same color, they are destroyed (and turn does not end).
  * Diagonal lines can also be collected.
  * But the orbs move only orthogonally.
- The orbs cannot get to a place where there will not be a continuous set of empty cells in their path.
 * You can step over small orbs in the process of movement.
- Every turn, 3 new small orbs appear, and 3 old small ones turn into big ones.
Defeat condition - the whole field will be filled with orbs.
Win condition - none, can play infinitely.


## TODO
- mind map classes connections. link to draw.io or picture

- Animations
- Pool Object
- Command Pattern (undo-redo)
- Bootstrapper
- StateMachine.
- Pathfind. Add Dijkstra and FloodFill
<!-- - Raname: Script 'Grid' has the same name as built-in Unity component. AddComponent and GetComponent will not work with this script.-->
<!--   
### Refactoring
- Architecture\ расцепить сильные связи везде где есть
- Board разбить на более мелкие классы
- Pathfind:
- - "while" to recursion
- - проверить, что работает если разрешить перемещения по диагонали (передавать не 4 соседние клетки, а 8)
 -->
### At ending
- Check all namespaces, delete unused usings.
- Delete ALL comments

<!--
## Naming
There may be an out-of-sync in the names "Ball -> Orb -> Item" or "Cell -> Node"

- Изначально писал код под игру Lines 98, но чтобы можно было переиспользовать, например для Match 3, изменил naming передвигаемой игровой еденицы в клетке с Ball на Item. В Match3 объекты часто не шарики - а ракушки, алмазы, бомбы, пончики... - для них обзий термин не Ball или Gem, а Item (с другой стороны, Item это слишком общо. FieldItem?)
  Вероятно где-то изменить забыл. todo сделать поиск по всему проекту


## My codestyle
`Rider settings -> Editor -> Code Style -> C#`


- `var` оставляю редко. У меня глаза сами сразу тип ищут в начале строки.

I prefer explicit type than `var`
- Пробелы внутри скобок для лучшей читаемости. Не люблю когда подряд мешанина из символов.

- Не ставлю отступ для класса. К сожалению, `File Scoped Namespaces` (например,  `namespace TestCSharp10;`) доступен только в c# 10 (в unity пока максимум c# 9), а лишний отступ я не хочу. Поэтому ставлю `Braces Layout -> Ident inside namespace declaration = false`.
-->



