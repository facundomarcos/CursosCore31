import React, { useState } from 'react';
import {IconButton, Toolbar, Typography, makeStyles, Button, Avatar, Drawer, List, ListItem, ListItemText} from '@material-ui/core';
import FotoUsuarioTemp from "../../../logo.svg";
import {useStateValue} from '../../../contexto/store';

//invoca las librerias responsive de material design
const useStyles = makeStyles((theme) => ({
    //para una computadora
    //display none, oculta todo
    //theme.breakpoints.up ->hasta que reconozca que esta en un dispositivo grande
    //entonces se haria flex
    seccionDesktop : {
        display : "none",
        [theme.breakpoints.up("md")] : {
            display : "flex"
        }

    },
    //empieza siendo un flex, pero si reconoce que es un dispositivo grande, lo oculta
    seccionMobile :{
        display : "flex",
        [theme.breakpoints.up("md")] : {
            display : "none"
        }
    },
    //toma todo el espacio disponible en un div
    grow : {
        flexGrow : 1
    },
    //define el tamaño de la foto del usuario
    avatarSize : {
        width : 40,
        height : 40
    },
    list : {
        width : 250
    },
    listItemText : {
        fontSize : "14px",
        fontWeight : 600,
        paddingLeft: "15px",
        color : "#212121"
    }
}))

const BarSesion = () => {
    const classes = useStyles();
    //traemos la sesion del usuario desde el index.js de los reducers
    const [{sesionUsuario}, dispatch] = useStateValue();
    //variable para abrir el drawer
    const [abrirMenuIzquierda, setAbrirMenuIzquierda] = useState(false);
    const cerrarMenuIzquierda = () => {
        setAbrirMenuIzquierda(false);
    }

    const abrirMenuIzquierdaAction = () => {
        setAbrirMenuIzquierda(true);
    }

    return (
        <React.Fragment>
             <Drawer 
                open = {abrirMenuIzquierda}
                onClose = {cerrarMenuIzquierda}
                anchor = "left"
             >
                 <div className = {classes.list} onKeyDown={cerrarMenuIzquierda} onClick={cerrarMenuIzquierda}>
                    <List>
                        <ListItem button>
                            <i className="material-icons">account_box</i>
                            <ListItemText  classes={{primary : classes.listItemText}} primary="Perfil" />
                        </ListItem>
                    </List>
                 </div>
                     

            </Drawer>

            <Toolbar>
                <IconButton color="inherit" onClick={abrirMenuIzquierdaAction}>
                    <i className="material-icons">menu</i>
                </IconButton>

                <Typography variant="h6">Cursos Online</Typography>
                <div className={classes.grow}></div>

                <div className={classes.seccionDesktop}>
                    <Button color="inherit">
                        Salir
                    </Button>
                    <Button color="inherit">
                        {sesionUsuario ? sesionUsuario.usuario.nombreCompleto : ""}
                    </Button>
                    <Avatar src={FotoUsuarioTemp}>

                    </Avatar>
                </div>

                <div className={classes.seccionMobile}>
                    <IconButton color="inherit">
                        <i className="material-icons">more_vert</i>
                    </IconButton>

                </div>
                
            </Toolbar>
        </React.Fragment>

       
    );
};

export default BarSesion;