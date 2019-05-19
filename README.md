# HashBucket
An experimental, totally inadvisable, 'roll-your-own' encryption paradigm for a 0-trust environment.

WARNING: This is experimental, I am not a security expert, and you SHOULD NOT expect this algorithm to be secure.
This application is a personal experiment and a foray into security. I expect and welcome any exploits/bugs/defects
that you may find here.

Quick paste-bin like service with anonymous encryption using a legible shared secret key.
```
Store
                             Client                                         Storage (exposed)
/----------------------------------------------------------------------\ /--------------------\
------------------------------------------------+--Encrypted Key Value-+-----------------------+
| message  \                                    |                      |                       |
|           encrypt message with password as => | EncrptedMessage      |                       |
|           /                     private key   |                      | Store EncryptedMessage|
| password <                                    |                      |   w/ HashKey as key   |
|           \ HASH  ==========================> | HashKey              |                       |
------------------------------------------------+----------------------+-----------------------+

Retrieve

            Client (Secure)                               Storage (exposed)
/--------------------------------\/---------------------------------------------\
+--Encrypted Key Value-----------+-----------------------------------------------+
|                                |                                               |
|                                |                                               |
| Password HASH   ============>  | HashKey => Fetch EncryptedMessage by HashKey  |
|                                |              in dictionary/db pattern         |
|                                |                      |                        |
+--------------------------------+----------------------v------------------------+
                                                        |
+--------------------------------+                      |
|                                |                      |
|                                |                     /
|     Decrypt Encrypted Value  <----------------------
|               with password    |
|                   |            |
|                   v            |
|           Original Message     |
+--------------------------------+
```
