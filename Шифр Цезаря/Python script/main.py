# This is a sample Python script.

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.

from Cesar1 import *
from Cesar2 import *


def main():
    message = "mage"
    key = "keyurt"
    offset = 3
    encrypted_message = cesar1(message, key, offset)

    print(encrypted_message)
    encrypted_message = cesar2(message, key)
    print(encrypted_message)

    pass


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    main()

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
