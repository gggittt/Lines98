# Lines98

Unity version - 2019.4.17f1. Потому что старые версии быстрее, на них проще прототипировать.


Для оценки знания c# кода достаточно, но в целом проект чуток незакончен. На техническом собеседовании можно попрактиковать live coding, реализую при Вас фичу на Ваш вкус. 

Комменты в коде не одобряю, и пишу только на время разработки, ближе к концу всё почищу. Как и неиспользуемые классы и методы для отладки.


##Bugs
LinesMatchComboChecker ошибка в определении индекса соседней клетки


##Game rules
Старая игра про сбор шаров в линии. При сборке более 5 шаров в линию одного цвета они уничтожаются (и ход не кончается).
Линии можно собирать и диагональные. Но двигаются шары только ортогонально. Шары не могут попать туда, где в их пути не будет непрерывного набора пустых клеток. Через мелкие шары переступать в процессе движения можно.
Каждый ход появляются 3 новых маленьких шара, а 3 старых маленьких превращаются в большие.
Условие поражения - всё поле заполнится шарами.


## TODO 
	translate Read.mi to english
	Architecture\ расцепить сильные связи везде где есть 

	Вернуть в проект Bootstrapper и StateMachine. 
	Стоило сразу использовать Zenject, было бы проще. 

	###В конце:
	Проверить все namespaces, удалить лишние using.
## Refactoring трудно читаемого
	class Board разбить на мелкие
	Pathfind
		to recursion


## Specific spots in my codestyle:
	Настраивается в Rider settings -> Editor -> Code Style -> C#
	
	Пробелы меж скобками, для лучшей читаемости. Не люблю когда подряд мешанина из символов.
	К сожалению, "File Scoped Namespaces" (напр "namespace TestCSharp10;") доступен только в c# 10 (в unity пока максимум c# 9), а лишний отступ я не хочу, поэтому ставлю Braces Layout -> Ident inside namespace declaration = false.
	var использую редко, у меня глаза сами сразу тип ищут в начале строки.

	Готов перестроить свой codestyle (обновлю настройки Райдера) при переходе в другую команду.


## Naming
	Изначально писал код под игру Lines 98, но чтобы можно было переиспользовать, например для Match 3, изменил naming передвигаемой игровой еденицы в клетке с Ball на Item. В Match3 объекты часто не шарики - а ракушки, алмазы, бомбы, пончики... - для них обзий термин не Ball или Gem, а Item 
		Вероятно где-то изменить забыл. todo сделать поиск по всему проекту
	Todo Raname: Script 'Grid' has the same name as built-in Unity component. AddComponent and GetComponent will not work with this script.

