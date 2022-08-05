import bpy
from bpy import context
import builtins as __builtin_
import csv
import os
from os import listdir
from os.path import isfile, join


class Actions:

    def __init__(self, name):
        self.name = name
        self.animations=[]
    
    def set_anim(self,anim):
        self.animations.append(anim)
        

folder_names = []
listactions = []

#-------------------------------------folder extraction code

MYDIR = "D:\College\Graduation\Animations"

for entry_name in os.listdir(MYDIR):
    entry_path = os.path.join(MYDIR, entry_name)
    if os.path.isdir(entry_path):
        action = Actions(entry_name)
        
        x =[join(MYDIR+"\\"+entry_name, f) for f in listdir(MYDIR+"\\"+entry_name) 
        if isfile(join(MYDIR+"\\"+entry_name, f))]
        
        action.set_anim(x)
        listactions.append(action)
 


for action in listactions:
    print(">>>>>>>>>>>>>>>>>>>>>>  "+action.name)
    for anim in action.animations:
        for i in anim:
            print("+  "+i)


def delete_hierarchy(obj):
    names = set([obj.name])

    # recursion
    def get_child_names(obj):
        for child in obj.children:
            names.add(child.name)
            if child.children:
                get_child_names(child)

    get_child_names(obj)

    print(names)
    objects = bpy.data.objects
    [setattr(objects[n], 'select', True) for n in names]

    bpy.ops.object.delete()



#------------------------------------animation extraction code

scene = bpy.context.scene

def console_print(*args, **kwargs):
    for a in context.screen.areas:
        if a.type == 'CONSOLE':
            c = {}
            c['area'] = a
            c['space_data'] = a.spaces.active
            c['region'] = a.regions[-1]
            c['window'] = context.window
            c['screen'] = context.screen
            s = " ".join([str(arg) for arg in args])
            for line in s.split("\n"):
                bpy.ops.console.scrollback_append(c, text=line)

outputFile ="C:\\Users\\SpaceMarco\\Desktop\\blender_output.csv"

d_arr = []

 
valueHx = ""
valueHy = ""
valueHz = ""
valueTx = ""
valueTy = ""
valueTz = ""

Head = ""
Tail = ""

with open(outputFile,'a', newline='') as f:
    csv.writer(f).writerow(["WFrame","Position","Head","Tail","Class"]) 
    
    for action in listactions:
        print(">>>>>>>>>>>>>>>>>>>>>>  "+action.name)
        for anim in action.animations:
            for i in anim:
                bpy.ops.import_scene.fbx( filepath = i ) #importing fbx
                
                bpy.ops.object.mode_set(mode = 'POSE')

                for fr in range(scene.frame_start, scene.frame_end+1):
                    
                    bpy.context.scene.frame_set(fr)
                    dict = {}
                    boneN="" 
                    valueHx = ""
                    valueHy = ""
                    valueHz = ""
                    valueTx = ""
                    valueTy = ""
                    valueTz = ""
                    pos =""

                    Head = ""
                    Tail = ""            
                    for bone in context.selected_pose_bones[:]:   
                        if 'Hand' in bone.name:
                            pass
                        else:
                            arrH_x = []
                            arrH_y = []
                            arrH_z = []
                            
                            arrT_x = []
                            arrT_y = []
                            arrT_z = []
                            
                            arr_x = []
                            arr_y = []
                            arr_z = []
                            
                            
                            tail = bpy.context.active_object.pose.bones[str(bone.name)].location+ bone.tail
                            head = bpy.context.active_object.pose.bones[str(bone.name)].location+ bone.head
                            
                            position = (tail + head)/2
                            
                            arr_x.append(position.x)
                            arr_y.append(position.y)
                            arr_z.append(position.z)
                            
                            arrH_x.append(head.x)
                            arrH_y.append(head.y)
                            arrH_z.append(head.z)

                            arrT_x.append(tail.x)
                            arrT_y.append(tail.y)
                            arrT_z.append(tail.z)

                            bone_name = bone.name
                            bone_name = bone_name[10:]
                            boneN = bone_name
                            
                            dict["Px:"+bone_name] = arr_x
                            dict["Py:"+bone_name] = arr_y
                            dict["Pz:"+bone_name] = arr_z
                            
                            dict["Hx:"+bone_name] = arrH_x
                            dict["Hy:"+bone_name] = arrH_y
                            dict["Hz:"+bone_name] = arrH_z
                            
                            dict["Tx:"+bone_name] = arrT_x
                            dict["Ty:"+bone_name] = arrT_y
                            dict["Tz:"+bone_name] = arrT_z
                            
                            dict["Frame"] = fr
                            
                            valuePx =  str(dict["Px:"+boneN]).replace('[','').replace(']','')
                            valuePy =  str(dict["Py:"+boneN]).replace('[','').replace(']','')
                            valuePz =  str(dict["Pz:"+boneN]).replace('[','').replace(']','')
                            
                            valueHx =  str(dict["Hx:"+boneN]).replace('[','').replace(']','')
                            valueHy =  str(dict["Hy:"+boneN]).replace('[','').replace(']','')
                            valueHz =  str(dict["Hz:"+boneN]).replace('[','').replace(']','')
                            
                            valueTx =  str(dict["Tx:"+boneN]).replace('[','').replace(']','')
                            valueTy =  str(dict["Ty:"+boneN]).replace('[','').replace(']','')
                            valueTz =  str(dict["Tz:"+boneN]).replace('[','').replace(']','')
                            
                            Head += "("+valueHx+","+valueHy+","+valueHz+")"
                            Tail += "("+valueTx+","+valueTy+","+valueTz+")"
                            
                            pos += "("+valuePx+","+valuePy+","+valuePz+")"
                            
                    csv.writer(f).writerow([str(fr),pos,Head,Tail,action.name])    

                if bpy.context.object.mode == 'POSE':
                    bpy.ops.object.mode_set(mode='OBJECT')
                 
                bpy.ops.object.select_all(action='SELECT')

                bpy.ops.object.delete()