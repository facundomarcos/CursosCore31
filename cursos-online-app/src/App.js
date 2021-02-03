import React from 'react';
//entrega las librerias de material design 
import {ThemeProvider as MuithemeProvider}  from "@material-ui/core/styles";
//importar el tema con los parametros que definimos del theme
import theme from "./theme/theme";
import RegistrarUsuario from './componentes/seguridad/RegistrarUsuario';
//import Login from './componentes/seguridad/Login';
//import PerfilUsuario from './componentes/seguridad/PerfilUsuario';

function App() {
return (
  <MuithemeProvider theme={theme}>
    <RegistrarUsuario />
  </MuithemeProvider>
  
)
}

export default App;
