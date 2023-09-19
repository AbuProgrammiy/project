def ielts_point(ls):
    res=sum(ls)/4
    tp=divmod(res,1)
    if tp[1] <= 0.250:
        res=int(res)
    elif tp[1] >0.250 and tp[1] <= 0.625:
        res=tp[0]+0.5
    else:
        res=tp[0]+1
    return res

import os
os.system("cls")
ls=list(map(float,input().split()))
if len(ls)==4:
    if max(ls)<10:
        if min(ls)>0:
            print(ielts_point(ls))
else:
    print("Ielts ballarini hato kiritingiz")
