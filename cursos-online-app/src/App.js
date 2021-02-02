import React from 'react';
//entrega las librerias de material design 
import MuithemeProvider from "@material-ui/core/styles/MuiThemeProvider";
//importar el tema con los parametros que definimos del theme
import theme from "./theme/theme";
import { Button, TextField } from '@material-ui/core';
import RegistrarUsuario from './componentes/seguridad/RegistrarUsuario';

function App() {
return (
  <MuithemeProvider theme={theme}>
    <RegistrarUsuario/>
  </MuithemeProvider>
  
)
}

export default App;
