# HashBucket
A dummy CRUD service for an experimental, totally inadvisable, 'roll-your-own' encryption paradigm for a 0-trust environment.
(find that here) https://github.com/manuella/HashBucketFrontend

WARNING: This is experimental, I am not a security expert, and you SHOULD NOT expect this algorithm to be secure.
This application is a personal experiment and a foray into security. I expect and welcome any exploits/bugs/defects
that you may find here.

Quick paste-bin like service with anonymous encryption using a legible shared secret key.
The idea is that the whatever service is storing the message cannot decrypt the message.

Message: the significant information to be transfered securely.
Password: a shared secret between two users
Hash: Some hash of the password which is unique per password.
EncryptedMessage: Message encrypted with Password as a private key

Client: Assumed to be secure / trusted
Storage: Completely untrusted


```
Store
                             Client                                         Storage (exposed)
/----------------------------------------------------------------------\ /--------------------\
------------------------------------------------+--Encrypted Key Value-+-----------------------+
| message  \                                    |                      |                       |
|           encrypt message with password as => | EncryptedMessage     |                       |
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
|                Message         |
+--------------------------------+
```


Example:
-Client stores a  key 'Foo' with a message 'Bar',
-Client hashses 'Foo' to '987b82r309uh32r' and encrypts 'Bar' with key 'Foo'
-Service receives hash key '987b82r309uh32r' and encrypted 'Bar'
  Client requests 'Foo' 
-Client retrieves message by hashing 'Foo' and navigating to that route '/987b82r309uh32r'.
-Service receives hashed key '987b82r309uh32r' and grabs encrypted message 'Bar' from that key
-Client unencrypts message '987b82r309uh32r' to 'Bar' with key 'Foo'

ASSUMPTION: In a perfect world, all potential hash keys return some valid encrypted body, but not in the current sql implementation.
(Garbage keys return indistinguishable garbage)

So what can we do with this?

If we add keys to the encrypted message, we can chain messages together.
This yields a graph of encrypted blocks with opaque topography.

For instance, given a single key, only its children (and recursively their children) can be accessed.
No information can be gleaned about the rest of the graph.

Let's say we're modeling a webportal for a hospital.

Given some authorization token and subject ID , we want to divulge all information about that subject they (owner of the auth token) are permitted to access.
+ A doctor should be able to access ALL information about their patients.
+ A patient should be able to access ALL information about themselves and their doctor, but not their other patients
+ A adminstrative assistant should be able to view any information about a doctor or patient that relates directly to scheduling, but no other details

'>' Designates that a particular node references (one way) another node
'#' refers the the given access token of a role (patient,doctor,adminstrative assistant)
'Get' ( requestor's auth token, subject ID token )

Patient Confidential > Patient Internal > Patient Contact

  
Doctor Confidential  > Doctor Internal  > Doctor Contact
                     > Patients


Get(



 



 

