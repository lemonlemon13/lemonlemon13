import socket
import time
import os
from _thread import *
from CryptoTools import *

def sendmsg(skt: socket.socket, name:str):
    while True:
        try:
            userIn = input()
            if (userIn == "--quit"):
                skt.close()
                break
            now = time.strftime("%d %b %Y %I:%M %p")
            msg = f"({now}) {name}: {userIn}"
            if len(msg) > 2048:
                print("Failed to Send. Message Too Long.")
                continue
            if userIn != "":
                skt.send(bytes(msg.encode("utf-8")))
        except ConnectionError as e:
            print("Connect Lost with Server.")
            exit

def main():
    name = input("Input Username (Max 16 Characters): ")
    while len(name) > 16:
        name = input("Input Username (Max 16 Characters): ")
    (ip, port) = RoomCode().CodeToAddr(input("Enter Lobby Code(e.g. ABCD-EFGH): "))

    skt = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    skt.connect((ip, port))
    try:
        skt.send(bytes(name.encode("utf-8")))
    except ConnectionError as e:
        print("Failed to connect to Server.")
        exit

    print("Type \"--quit\" to leave the room.")
    try:
        start_new_thread(sendmsg, (skt,name,))
        while True:
            data = str(skt.recv(16*1024),encoding="utf-8")
            os.system('cls')
            print(data)
    except ConnectionError as e:
        print("Connect Lost with Server.")

if __name__ == "__main__":
    main()