def transformer(ls):
    c=0
    c1=0
    co=1
    for i in ls:
        if co == 1:
            c+=i
        elif co== 2:
            c1=c1+i
        elif co == 3:
            c=c-i
        elif co==4:
            c1=c1-i
        if co<4:
            co+=1
        else:
            co=1 
    return c1,c

import os

os.system("cls")
ls=list(map(int,input("Enter: ").split()))
print(transformer(ls))