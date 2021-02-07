import React from 'react';
//entrega las librerias de material design 
import {ThemeProvider as MuithemeProvider}  from "@material-ui/core/styles";
import {Grid}  from "@material-ui/core";
//importar el tema con los parametros que definimos del theme
import theme from "./theme/theme";
import RegistrarUsuario from './componentes/seguridad/RegistrarUsuario';
import PerfilUsuario from './componentes/seguridad/PerfilUsuario';
import Login from './componentes/seguridad/Login';
//enrutador, componente, ruta
import {BrowserRouter as Router, Switch, Route} from 'react-router-dom';
import AppNavbar from './componentes/navegacion/AppNavbar';

function App() {
return (
  <Router>
    <MuithemeProvider theme={theme}>
      <AppNavbar />
      <Grid container>
        <Switch>
          <Route exact path="/auth/login" component={Login} />
          <Route exact path="/auth/registrar" component={RegistrarUsuario} />
          <Route exact path="/auth/perfil" component={PerfilUsuario} />
          <Route exact path="/" component={PerfilUsuario} />
        </Switch>

      </Grid>
     



    </MuithemeProvider>
  </Router>

  
)
}

export default App;
