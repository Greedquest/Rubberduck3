# Rubberduck3 LSP Client

### Overview
The client is one part of the language server protocol (LSP)

If the language server interprets the code and provides the red squiggles, then the client displays them.

The RD client has 2 parts:
 1. A VBE addin (this used to be the whole of Rubberduck before version 3). This launches and closes Rubberduck, and wires up any buttons or other interaction with the VBE.
 2. The RD Editor, which does everything the VBE used to do like display editable VBA code and various menus. When you interact with the RD editor, it streams notifications to the language server about what edits you made to the code, and the server responds back with helpful messages, according to have LSP specification.

### In this folder
tbc...
