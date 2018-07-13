import os
import imageio
import moviepy.editor as mp


png_dir = "/Users/robertkatz/Desktop/elrowPics/"
images = []
for subdir, dirs, files in os.walk(png_dir):
    for file in files:
        file_path = os.path.join(subdir, file)
        if file_path.endswith(".png"):
            images.append(imageio.imread(file_path))
imageio.mimsave('./hhh.gif', images)
clip = mp.VideoFileClip("./hhh.gif")
clip.write_videofile("./hhh.ogv",bitrate="12000k")
print("hello world")