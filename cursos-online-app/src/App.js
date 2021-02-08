import React, { useState, useEffect } from 'react';
//entrega las librerias de material design 
import {ThemeProvider as MuithemeProvider}  from "@material-ui/core/styles";
import {Grid, Snackbar}  from "@material-ui/core";
//importar el tema con los parametros que definimos del theme
import theme from "./theme/theme";
import RegistrarUsuario from './componentes/seguridad/RegistrarUsuario';
import PerfilUsuario from './componentes/seguridad/PerfilUsuario';
import Login from './componentes/seguridad/Login';
//enrutador, componente, ruta
import {BrowserRouter as Router, Switch, Route} from 'react-router-dom';
import AppNavbar from './componentes/navegacion/AppNavbar';
import { useStateValue } from './contexto/store';
import { obtenerUsuarioActual } from './actions/UsuarioAction';

function App() {
  //obtener sesion de usuario
  //dispatch es una representacion del contexto
   //referencia a la clase global snackbar
  const [{sesionUsuario, openSnackbar}, dispatch] = useStateValue();

  //variable local
  //para saber si el request fue o no hecho al servidor
  const [iniciaApp, setInicialApp] = useState(false);
  //una vez que se haya cargado el iniciaapp lo evalua
  useEffect(() => {
    if(!iniciaApp){
      //que vaya al servidor y traiga el usuario actual
      obtenerUsuarioActual(dispatch).then(response => {
        setInicialApp(true);
      }).catch(error => {
        setInicialApp(true);
      })
    }
  }, [iniciaApp])

return (

  <React.Fragment>
    <Snackbar 
      anchorOrigin={{ vertical:"bottom", horizontal:"center" }}
      open={openSnackbar ? openSnackbar.open : false}
      autoHideDuration={3000}
      ContentProps={{"aria-describedby": "message-id"}}
      message = {
        <span id="message-id">{openSnackbar ? openSnackbar.mensaje : "" }</span>
      }
      onClose= { () => 
        dispatch({
          type : "OPEN_SNACKBAR",
          openMensaje : {
            open : false,
            mensaje : ""
          }
        })
      }
    >

    </Snackbar>
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
  </React.Fragment>



  
)
}

export default App;
