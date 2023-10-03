| [Game rules](#Game-rules) | [TODO](#TODO) | [Refactoring](#Refactoring) |[Naming](#Naming) | [My codestyle](#My-codestyle) |
|---------------------------|---------------|-----------------------------|------------------|-------------------------------|

# Lines98

Unity version - 2019.4.17f1. Потому что старые версии быстрее, на них проще прототипировать.

Проект в процессе разработки. На техническом собеседовании можно попрактиковать live coding, реализую при Вас фичу на Ваш вкус.


## Game rules
[Play Online (version with small orbs)](https://www.min2win.ru/gms/834.html)

Старая игра про сбор шаров в линии. При сборке более 5 шаров в линию одного цвета они уничтожаются (и ход не кончается).
Линии можно собирать и диагональные. Но двигаются шары только ортогонально. Шары не могут попать туда, где в их пути не будет непрерывного набора пустых клеток. Через мелкие шары переступать в процессе движения можно.
Каждый ход появляются 3 новых маленьких шара, а 3 старых маленьких превращаются в большие.
Условие поражения - всё поле заполнится шарами.

<!-- 
## Bugs
fixed - LinesMatchComboChecker ошибка в определении индекса соседней клетки
-->


## TODO
- translate READMI.md to english
- mind map classes connections. link to draw.io or picture

- Animations
- Pool Object
- Command Pattern (undo-redo)
- Вернуть в проект Bootstrapper и StateMachine.
- Pathfind Добавить Dijkstra and FloodFill
<!-- - Raname: Script 'Grid' has the same name as built-in Unity component. AddComponent and GetComponent will not work with this script.-->
<!--   
### Refactoring
- Architecture\ расцепить сильные связи везде где есть
- Board разбить на более мелкие классы
- Pathfind:
- - while to recursion
- - проверить, что работает если разрешить перемещения по диагонали (передавать не 4 соседние клетки, а 8)
 -->
### В конце
Проверить все namespaces, удалить лишние using.

<!--   
## Naming
- Изначально писал код под игру Lines 98, но чтобы можно было переиспользовать, например для Match 3, изменил naming передвигаемой игровой еденицы в клетке с Ball на Item. В Match3 объекты часто не шарики - а ракушки, алмазы, бомбы, пончики... - для них обзий термин не Ball или Gem, а Item
  Вероятно где-то изменить забыл. todo сделать поиск по всему проекту
-->

## My codestyle
Настраивается в `Rider settings -> Editor -> Code Style -> C#`

- `var` оставляю редко. У меня глаза сами сразу тип ищут в начале строки.
<!-- 
- Пробелы внутри скобок для лучшей читаемости. Не люблю когда подряд мешанина из символов.
-->
- К сожалению, `File Scoped Namespaces` (например,  `namespace TestCSharp10;`) доступен только в c# 10 (в unity пока максимум c# 9), а лишний отступ я не хочу. Поэтому ставлю `Braces Layout -> Ident inside namespace declaration = false`.


Готов перестроить свой codestyle (обновлю настройки Райдера) при переходе в другую команду.

