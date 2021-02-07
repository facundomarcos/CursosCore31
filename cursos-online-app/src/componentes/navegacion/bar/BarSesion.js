import React from 'react';
import {IconButton, Toolbar, Typography, makeStyles, Button, Avatar} from '@material-ui/core';
import FotoUsuarioTemp from "../../../logo.svg"

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
    }
}))

const BarSesion = () => {
    const classes = useStyles();

    return (
        <Toolbar>
            <IconButton color="inherit">
                <i className="material-icons">menu</i>
            </IconButton>

            <Typography variant="h6">Cursos Online</Typography>
            <div className={classes.grow}></div>

            <div className={classes.seccionDesktop}>
                <Button color="inherit">
                    Salir
                </Button>
                <Button color="inherit">
                    {"Nombre de Usuario"}
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
    );
};

export default BarSesion;