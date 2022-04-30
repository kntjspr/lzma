# lzma
Lightweight version of Citadel.

## File structure
![File Structure](https://raw.githubusercontent.com/kntjspr/Citadel/main/Github/file-structure.png)

### Arguments

Citadel.exe [path of the file you wish to encrypt/decrypt]  [path to the directory in which the result will be saved] [-e/-d (encrypt/decrypt)] [password (optional)] [File Extension (Optional)] 

- File Extension argument are ignored for -d/decrypt args since [the original filename will be used](https://github.com/kntjspr/Citadel#features)

### Encrypting file example:
``Citadel.exe C:\DecryptedFolder\file.exe C:\EncryptedFolder -e password7737 cit``

- Output sample: C:\EncryptedFolder\\dbcsiwpqen.cit

### Decrypting file example:
``Citadel.exe C:\EncryptedFolder\dbcsiwpqen.cit C:\DecryptedFolder -d password7737``

- Output sample: C:\DecryptedFolder\file.exe
