# nameOfFile0 = "D:\\INTERNET\\Wiki-Andresito-16-WORK\\PYTHONNOUNITY\\FilesItalianGen\\ITALIANGEN-1\\texto"
nameOfFile0 = "outputWordUnityOneLanguage"


listOfWordOneLanguage = list()

f0 = open(nameOfFile0 + ".txt", "r+")


for i in f0:
	
	listOfWordOneLanguage.append(i.upper())


f0.close()



print(listOfWordOneLanguage)



# nameOfFile0 = "D:\\INTERNET\\Wiki-Andresito-16-WORK\\PYTHONNOUNITY\\FilesItalianGen\\ITALIANGEN-1\\texto"
nameOfFile1 = "outputWordUnityOneLanguage"



fileOutput = open(nameOfFile1 + ".txt", "w+")


for i in listOfWordOneLanguage:
	fileOutput.write(i)

fileOutput.close()
	


# nameOfFile0 = "D:\\INTERNET\\Wiki-Andresito-16-WORK\\PYTHONNOUNITY\\FilesItalianGen\\ITALIANGEN-1\\texto"
nameOfFile0 = "outputWordUnityTwoLanguage"


listOfWordTwoLanguage = list()

f0 = open(nameOfFile0 + ".txt", "r+")


for i in f0:
	
	listOfWordTwoLanguage.append(i.upper())


f0.close()



print(listOfWordTwoLanguage)



# nameOfFile0 = "D:\\INTERNET\\Wiki-Andresito-16-WORK\\PYTHONNOUNITY\\FilesItalianGen\\ITALIANGEN-1\\texto"
nameOfFile1 = "outputWordUnityTwoLanguage"



fileOutput = open(nameOfFile1 + ".txt", "w+")


for i in listOfWordTwoLanguage:
	fileOutput.write(i)

fileOutput.close()
	






