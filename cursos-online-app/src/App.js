import React from 'react';
//entrega las librerias de material design 
import MuithemeProvider from "@material-ui/core/styles/MuiThemeProvider";
//importar el tema con los parametros que definimos del theme
import theme from "./theme/theme";
import { Button, TextField } from '@material-ui/core';

function App() {
return (
  <MuithemeProvider theme = {theme}>
    <h1>Proyecto en blanco</h1>
    <TextField variant = "outlined"/>
    <Button variant="contained" color="primary">Mi boton material design</Button>
  </MuithemeProvider>
  
)
}

export default App;
