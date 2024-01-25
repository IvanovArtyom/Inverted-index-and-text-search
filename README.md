# Инвертированный индекс и текстовый поиск
## Задача
На вход программе передается путь до директории, в которой находится несколько текстовых файлов. Каждый файл представляет из себя набор слов (лексем), разделенных пробелами, табуляциями и переносами строк. Каждая лексема представляет из себя набор букв и цифр. Все лексемы нумеруются, начиная с нуля, по порядку следования в файле, занимая таким образом определенные позиции в файле.

Необходимо прочитать содержимое всех файлов и составить инвертированный индекс. Инвертированный индекс представляет собой список элементов вида <лексема, статистическая информация>, отсортированный по лексемам в алфавитном порядке. Статистическая информация - это структура данных, описывающая вхождение лексемы в текстовые файлы (имя файла, общее количество вхождения лексемы в файл), а также детализирующая на каких позициях в файле (под какими номерами) встречается данная лексема.

Далее необходимо в командной строке запросить у пользователя слово (лексему) для поиска, после чего с помощью инвертированного индекса выдать список имен файлов, в которых данное слово присутствует, с указанием количества вхождений запрашиваемого слова в каждый из файлов. Список файлов в выдаче должен быть отсортирован по количеству вхождений данного слова в файлы, т.е. на первом месте в выводе должен стоять файл, где указанное слово встречается чаще всего, на втором – второй по количеству вхождений данного слова и т.д. На последнем месте будет выводиться имя файла, в котором данное слово присутствует меньше всего раз (имена файлов с нулевым количеством вхождений не выводятся).

Далее необходимо запросить у пользователя номер строки в выдаче, после чего распечатать детализацию вхождения введенного ранее слова в файл, соответствующий введенному номеру. Детализация должна представлять из себя список позиций лексемы внутри файла, на которых встречается запрашиваемое слово.
