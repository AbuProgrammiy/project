import os
os.system("cls")

ls=list(map(str,input().split()))
ls.sort()
for i in range(len(ls)):
    if ls[i].isdigit():
        ls[i]=int(ls[i])
        
print(ls)