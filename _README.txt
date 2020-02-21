==Overwiew==
CsvSummator by Daniel "Aramil" Dowgiert
Application reading data from csv file and summing it into separate categories.
Expected file structure: valid_date ; category ; dropped_data ; quantity

*Application tries to parse date and should work with every date used in local pc culture.
*Category can be any string
*Quantity is limited to integer data range
Row which fails to parse in any of this categories will be dropped.


Applicaton returns data in categories:
- Overall sum - all data
- Sums by date - data grouped by years (extracted from date)
- Sums by category - data grouped accordlingly to second column in source file (case sensitive)

Output file template (Application uses Polish category names):
1st line: Typ sumy;Identyfikator;Wartość
next lines: category_type<string>;category_key<string>;sum<int>

Application is delivered in two versions:
- RC - standard version, should be at least quite responsive every time. App provides only important information while working (summaries, errors).
- Debug - version providing more information, may be unresponsive for long intervals. Informs ie. about dropped lines in files, may be used ie. to find improperly prepared file


===Integrator Manual (Command Line)==
Application can use command line arguments. Supported arguments:
--Path="path" <string> - source path with data files
--NoticeTime=time_in_s <int> - time between reporting current status in Log textbox (result is always reported when parsing finishes)
--ThreadCount=number <int> - number of consecutive threads used by Application functionality (+1 for GUI +1 for utility purposes)
--Autostart=state <bool> - should Application begin work on startup (App doesn't support independent work, user will still need to chose path to save file)
*incorrect arguments should be safely discarded


==User Manual (Graphic Interface)==

Application provides user controls:
- "Folder startowy" textbox - path to folder with data to parse
- "Powiadamiaj co (s)" - time in seconds between status summary in Log
- "Ilość wątków" - number of consecutive threads when application processes data. WARNING: big numbers can make app unresponsive
- "Ręcznie wczytaj nową listę plików" - button function depends from state of application:
*if app is currently processing data, clicking this button will parse "Folder startowy" folder and add found files to current queue
*if app finished work, clicking this button will parse "Folder startowy" folder and prepare its contains for new parsing
- "Wyczyść log" - button clears Log visible in app window
- "START" - button allows to start or continue work

Application contains two elements intended solely for read:
- Status label showing overall state of application
- Log textbox showing detailed informations and allowing to copy its contents

Known limitations and problems:
- Folders with more than 2000 files may not be parsed correctly, especially when added to existing queue - check Log for information if parse is completed succesfully
- Application should work with folders used by other apps. But obviously won't give correct summary (Program don't try again to parse refused files - only notification is Log entry)
- In rare cases, program may have problem with closing all Threads and enter loop of waiting. In this case, press "START" again.