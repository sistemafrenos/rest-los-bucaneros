' Este modulo contiene el c�digo a disposicion por parte de IFDRIVERS
' en una base TAL CUAL. Todo receptor del Modulo se considera
' bajo licencia de los derechos de autor de IFDRIVERS para utilizar el
' codigo fuente siempre en modo que el o ella considere conveniente,
' incluida la copia, la compilacion, su modificacion o la redistribucion,
' con o sin modificaciones. Ninguna licencia o patentes de IFDRivers
' este implicita en la presente licencia.
'
' El usuario del codigo fuente debera entender que IFDRIVERS no puede
' Proporcionar apoyo tecnico para el modulo y no sera Responsable
' de las consecuencias del uso del programa.
'
' Todas las comunicaciones, incluida esta, no deben ser removidos
' del modulo sin el consentimiento previo por escrito de IFDRIVERS
' www: http://www.impresoras-fiscales.com.ar/
' email: soporte@impresoras-fiscales.com.ar
'
' Instrucciones para usar las funciones de alto nivel en VB.NET:
'
' 1) Agregue el archivo con esta clase a su proyecto.
' 2) Agregue una referencia al componente fiscal FiscalNET.DLL en su proyecto.
' 3) Declare y cree la clase en su codigo.
' Todas las funciones del componente FiscalNET.dll seran accesibles tambien 
' desde esta clase: IF_OPEN, IF_CLOSE,etc. mas las funciones de alto nivel.
' 
' Por ejemplo:
'
' Dim m_objEpsonVE As EpsonVE = new EpsonVE();
'
' Dim nError As Integer = m_objEpsonVE.IF_OPEN("COM1",9600);
'
' ....etc
'
Namespace EpsonVE

 Public Class EpsonVE: Inherits FiscalNET.DriverFiscal
  Public Sub New()
   Printer = "PF220"
  End Sub

  '*******************************************************************************
  '1. Comandos de Control Fiscal
  '*******************************************************************************
  ''' <summary>Consulta de estado</summary>
  ''' <remarks>Este comando es utilizado para evaluar el estado del controlador fiscal. Tambi�n permite definir el comportamiento del equipo a las se�ales provenientes de los sensores de papel. Podr� ser ejecutado despu�s y antes de cualquier otro comando.</remarks>
  ''' <param name='byVar1'>Tipo de informaci�n solicitada {NEABCDRFJSU}</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function StatusRequest(byVar1 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@StatusRequest" & "|" & byVar1	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Cierre de jornada fiscal</summary>
  ''' <remarks>Este comando imprime un reporte con los totales almacenados en la memoria de trabajo. Los montos pueden ser de los totales diarios (reporte Z) o de los totales parciales acumulados desde la emisi�n del
�ltimo reporte X. Al realizar un reporte Z los montos almacenados en la memoria de trabajo son llevados a la memoria fiscal.</remarks>
  ''' <param name='byVar1'>Tipo de reporte {ZX}</param>
  ''' <param name='byVar2'>parametro de impresion</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function DailyClose(byVar1 As String, byVar2 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@DailyClose" & "|" & byVar1 & "|" & byVar2	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Reporte de auditoria por fechas</summary>
  ''' <remarks>Este comando permite generar un reporte que incluye los diferentes cierres diarios que han sido almacenados en la memoria fiscal durante el periodo de fechas seleccionado. Este comando usa tiempo extendido para su finalizaci�n. El reporte puede ser detallado o un resumen mensual.Adicionalmente, utilizando el calificador del comando con la opci�n �C� la respuesta al comando contiene el rango de cierres diarios asociados al per�odo de fechas. Este comando efect�a una verificaci�n del contenido de la memoria fiscal. Su ejecuci�n puede tardar varios minutos.</remarks>
  ''' <param name='strVar1'>Fecha de inicio de selecci�n AAMMDD (max 6 bytes)</param>
  ''' <param name='strVar2'>Fecha de fin de selecci�n AAMMDD (max 6 bytes)</param>
  ''' <param name='byVar3'>Calificador de reporte {DMRC}</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function DailyCloseByDate(strVar1 As String, strVar2 As String, byVar3 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@DailyCloseByDate" & "|" & strVar1 & "|" & strVar2 & "|" & byVar3	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Reporte de auditoria por numero</summary>
  ''' <remarks>Este comando permite generar un reporte conformado por una secuencia de cierres diarios. Este comando usa tiempo extendido para su finalizaci�n.</remarks>
  ''' <param name='nVar1'>N�mero de Z de inicio de selecci�n (nnnn)</param>
  ''' <param name='nVar2'>N�mero de Z de fin de selecci�n (nnnn)</param>
  ''' <param name='byVar3'>Calificador de reporte</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function DailyCloseByNumber(nVar1 As Integer, nVar2 As Integer, byVar3 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@DailyCloseByNumber" & "|" & Str(nVar1) & "|" & Str(nVar2) & "|" & byVar3	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  '*******************************************************************************
  '2. Comandos para generar comprobantes fiscales
  '*******************************************************************************
  ''' <summary>Abrir comprobante fiscal</summary>
  ''' <remarks>Este comando es el primer paso para producir un comprobante fiscal. Se imprime el encabezado, el n�mero, y la fecha y la hora de emisi�n del comprobante fiscal. Esta informaci�n se registra en la
memoria de trabajo.Se rechazar� el comando si hay un comprobante fiscal abierto, si la memoria fiscal est� llena, si hay un error en la memoria de trabajo, o si es necesario realizar un cierre de jornada (Reporte Z).
Este comando puede ser utilizado para generar comprobantes de devoluci�n (notas de cr�dito), para esto se el calificador del comando (campo Nro 5) debe ser igual a 'D'. Si se est� realizando una Nota de Cr�dito son permitidos todos los comandos que aplican para un comprobante
fiscal normal, excepto el comando ReturnRecharge.</remarks>
  ''' <param name='strVar1'>Raz�n social 1 (max 40 bytes)</param>
  ''' <param name='strVar2'>RIF del comprador (max 20 bytes)</param>
  ''' <param name='strVar3'>Nro de comprobante (en devoluci�n) (max 20 bytes)</param>
  ''' <param name='strVar4'>Serial de la maquina fiscal que realizo el comprobante en devoluci�n (Solo en nota de cr�dito) (max 20 bytes)</param>
  ''' <param name='strVar5'>Fecha del comprobante en devoluci�n (Solo en nota de cr�dito) (max 6 bytes)</param>
  ''' <param name='strVar6'>Hora del comprobante en devoluci�n (max 6 bytes)</param>
  ''' <param name='byVar7'>Tipo de documento {TD}</param>
  ''' <param name='byVar8'>Campo reservado</param>
  ''' <param name='byVar9'>Campo reservado</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function OpenFiscalReceipt(strVar1 As String, strVar2 As String, strVar3 As String, _
                            strVar4 As String, strVar5 As String, strVar6 As String, _
                             byVar7 As String, byVar8 As String, byVar9 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@OpenFiscalReceipt" & "|" & strVar1 & "|" & strVar2 & "|" & strVar3 & "|" & _
            strVar4 & "|" & strVar5 & "|" & strVar6 & "|" & byVar7 & "|" & _
             byVar8 & "|" & byVar9	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Imprimir texto fiscal</summary>
  ''' <remarks>Se rechazar� el comando si no hay un comprobante fiscal abierto. El texto s�lo puede ser 'texto fiscal', y debe tener una longitud m�xima limitada de manera que no se pueda imprimir nada en las columnas que
normalmente est�n ocupadas por campos de montos de �tems de l�neas. No se pueden imprimir m�s de 3 l�neas de texto fiscal consecutivas. Si se efect�o previamente un cierre parcial de un comprobante fiscal
se permitir� ejecutar el comando 0x41. Se debe ejecutar de nuevo el comando CloseFiscalReceipt para finalizar el comprobante. Si el campo 2 es igual 'S' no se imprime el texto en la cinta de
auditoria.</remarks>
  ''' <param name='strVar1'>Texto Fiscal a Imprimir (max 26 bytes)</param>
  ''' <param name='byVar2'>Calificador de Impresi�n {SO}</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function PrintFiscalText(strVar1 As String, byVar2 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@PrintFiscalText" & "|" & strVar1 & "|" & byVar2	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Imprimir item</summary>
  ''' <remarks>No se aceptar� el comando si no hay un comprobante fiscal abierto. Se rechazar� si la acumulaci�n de montos genera un desborde de totales. El texto se encuentra limitado a 'texto fiscal' (se pueden utilizar
los efectos de impresi�n, solo para la descripci�n del �tem).</remarks>
  ''' <param name='strVar1'>Descripci�n de hasta 20 caracteres (max 20 bytes)</param>
  ''' <param name='dblVar2'>Cantidad (nnnn.nnn)</param>
  ''' <param name='dblVar3'>Monto del �tem (nnnnnnnn.nn)</param>
  ''' <param name='dblVar4'>Tasa impositiva (.nnnn)</param>
  ''' <param name='byVar5'>Calificador de �tem de l�nea {Mm}</param>
  ''' <param name='byVar6'>Reservado</param>
  ''' <param name='byVar7'>Reservado</param>
  ''' <param name='byVar8'>Reservado</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function PrintLineItem(strVar1 As String, dblVar2 As Double, dblVar3 As Double, dblVar4 As Double, _
                        byVar5 As String, byVar6 As String, _
                         byVar7 As String, byVar8 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@PrintLineItem" & "|" & strVar1 & "|" & Str(dblVar2) & "|" & Str(dblVar3) & "|" & _
            Str(dblVar4) & "|" & byVar5 & "|" & byVar6 & "|" & byVar7 & "|" & _
             byVar8	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Subtotal</summary>
  ''' <remarks>Este comando ser� rechazado si no hay un comprobante fiscal abierto. Este comando es �til para verificar que los montos acumulados en la impresora fiscal, a trav�s del proceso de facturaci�n,
concuerdan con los llevados por el software en el host. Luego de este comando se pueden emitir comandos de impresi�n de �tem adicionales.</remarks>
  ''' <param name='byVar1'>Reservado</param>
  ''' <param name='byVar2'>Reservado</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function Subtotal(byVar1 As String, byVar2 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@Subtotal" & "|" & byVar1 & "|" & byVar2	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Pago,Cancelar y Descuento en Comprobante fiscal</summary>
  ''' <remarks>Se rechazar� este comando si no hay un comprobante fiscal abierto, si los montos acumulados generan un desbordamiento de total. Se usa este comando para imprimir informaci�n del total y del pago de la
transacci�n. Despu�s de este comando, no se pueden emitir comandos de impresi�n de l�nea de �tem adicionales.</remarks>
  ''' <param name='strVar1'>Descripci�n de 20 caracteres (max 20 bytes)</param>
  ''' <param name='dblVar2'>Monto de pago (nnnnnn.nn)</param>
  ''' <param name='byVar3'>Calificador de comando {CTDP}</param>
  ''' <param name='dblVar4'>Tasa impositiva sobre la que aplica la promoci�n (.nnnn)</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function ReturnRecharge(strVar1 As String, dblVar2 As Double, byVar3 As String, dblVar4 As Double) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@ReturnRecharge" & "|" & strVar1 & "|" & Str(dblVar2) & "|" & byVar3 & "|" & _
            Str(dblVar4)	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Cerrar comprobante fiscal</summary>
  ''' <remarks>Se rechazar� el comando si no hay un comprobante fiscal abierto, o si los montos acumulativos originan un desbordamiento del total. Este comando se usa para cerrar el comprobante fiscal, acumular totales en
memoria de trabajo, imprimir el importe total y los impuestos, el logotipo fiscal y el serial del equipo.</remarks>
  ''' <param name='byVar1'>Calificador de comando {AE}</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function CloseFiscalReceipt(byVar1 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@CloseFiscalReceipt" & "|" & byVar1	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  '*******************************************************************************
  '3. Comandos para generar documentos no fiscales
  '*******************************************************************************
  ''' <summary>Abrir comprobante no-fiscal</summary>
  ''' <remarks>Se debe utilizar este comando para comenzar a imprimir un documento no fiscal. Se imprime en el
encabezado y el n�mero del documento. El comando ser� rechazado si hay un comprobante fiscal o un
documento no fiscal abierto.</remarks>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function OpenNonFiscalReceipt() As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@OpenNonFiscalReceipt"	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Imprimir texto no-fiscal</summary>
  ''' <remarks>El comando ser� rechazado si no est� abierto un documento no fiscal. Se restringir� el texto al conjunto
de caracteres definidos como �texto fiscal�.</remarks>
  ''' <param name='strVar1'>Hasta 40 caracteres de texto fiscal (max 40 bytes)</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function PrintNonFiscalText(strVar1 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@PrintNonFiscalText" & "|" & strVar1	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Cerrar comprobante no-fiscal</summary>
  ''' <remarks>El comando ser� rechazado si no est� abierto un documento no fiscal. Este comando se utiliza para cerrar el documento no fiscal.
Si el calificador de comando es �E�, se termina el documento, se corta el papel y se imprimen las primeras l�neas descriptivas del pr�ximo comprobante fiscal. Despu�s de cerrar el documento de esta
manera solo se podr� emitir un comprobante fiscal, en caso contrario se deber� cortar el papel.</remarks>
  ''' <param name='byVar1'>Calificador de comando. 'E' = indica cierre econ�mico. Se imprime encabezado del pr�ximo comprobante</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function CloseNonFiscalReceipt(byVar1 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@CloseNonFiscalReceipt" & "|" & byVar1	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  '*******************************************************************************
  '4. Comandos de control de la impresora
  '*******************************************************************************
  ''' <summary>Cortar papel</summary>
  ''' <remarks>Este comando se utiliza para cortar el papel de recibo. Los comprobantes fiscales, los documentos no fiscales y los reportes con cortados autom�ticamente al finalizar los comandos respectivos</remarks>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function PaperCut() As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@PaperCut"	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Avanzar papel de tickets</summary>
  ''' <remarks>Este comando hace avanzar el papel de recibo.</remarks>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function FeedReceipt() As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@FeedReceipt"	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Activar Split</summary>
  ''' <remarks>Este comando activa el funcionamiento del Slip. Debe ser ejecutado antes de cualquier otro comando relacionado con relacionado con el manejo del Slip.</remarks>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function ActivateSlip() As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@ActivateSlip"	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Este comando desactiva el funcionamiento del Slip.</summary>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function InActivateSlip() As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@InActivateSlip"	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Este comando imprime por el Slip en el formato de un cheque.</summary>
  ''' <param name='strVar1'>Monto del Cheque (max 12 bytes)</param>
  ''' <param name='strVar2'>Beneficiario (max 40 bytes)</param>
  ''' <param name='strVar3'>Fecha de emisi�n (max 20 bytes)</param>
  ''' <param name='byVar4'>'E' = Se imprime la frase 'NO ENDOSABLE', 'R' = Se imprime la frase 'NO ENDOSABLE' en negrita {ER}</param>
  ''' <param name='byVar5'>Separacion entre lineas monto y benficiario(1 al 7)</param>
  ''' <param name='byVar6'>Separacion entre 'la cantidad' y la fecha(1 al 7)</param>
  ''' <param name='byVar7'>Separacion entre 'no endosable' y el monto superior(1 al 7)</param>
  ''' <param name='byVar8'>Separacion entre 'beneficiario' y la cantidad ( 1 al 7)</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function FormatoCheque(strVar1 As String, strVar2 As String, strVar3 As String, byVar4 As String, _
                        byVar5 As String, byVar6 As String, _
                         byVar7 As String, byVar8 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@FormatoCheque" & "|" & strVar1 & "|" & strVar2 & "|" & strVar3 & "|" & _
            byVar4 & "|" & byVar5 & "|" & byVar6 & "|" & byVar7 & "|" & byVar8	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Este comando imprime por el Slip el endoso para un cheque.</summary>
  ''' <param name='strVar1'>Texto a imprimir (max 33 bytes)</param>
  ''' <param name='strVar2'>Texto a imprimir (max 40 bytes)</param>
  ''' <param name='strVar3'>Texto a Imprimir (max 40 bytes)</param>
  ''' <param name='byVar4'> {ABC}</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function FormatoEndoso(strVar1 As String, strVar2 As String, strVar3 As String, byVar4 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@FormatoEndoso" & "|" & strVar1 & "|" & strVar2 & "|" & strVar3 & "|" & _
            byVar4	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  '*******************************************************************************
  '5. Comandos generales
  '*******************************************************************************
  ''' <summary>Ingresar fecha y hora </summary>
  ''' <remarks>Este comando establece la fecha y hora del reloj de tiempo real del controlador fiscal, la cual se estampa en todos los documentos fiscales y en todas las entradas de la memoria fiscal.</remarks>
  ''' <param name='strVar1'>Formato de Fecha AAMMDD (A�o, Mes, D�a) (max 6 bytes)</param>
  ''' <param name='strVar2'>Formato de Hora HHMMSS (Hora, Minutos, Segundos) (max 6 bytes)</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function SetDateTime(strVar1 As String, strVar2 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@SetDateTime" & "|" & strVar1 & "|" & strVar2	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Consultar fecha y hora</summary>
  ''' <remarks>Este comando devuelve la fecha y hora del reloj de tiempo real del controlador fiscal.</remarks>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function GetDateTime() As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@GetDateTime"	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Programar texto de encabezamiento </summary>
  ''' <remarks>Este comando almacena un l�nea de datos fijos que aparece en el encabezado o pie de p�gina de los comprobantes fiscales. Se permite hasta un m�ximo de 5 l�nea para el encabezado y para el pie de p�gina.</remarks>
  ''' <param name='nVar1'>N�mero de l�nea de datos fijos (nn)</param>
  ''' <param name='strVar2'>Texto Fiscal de hasta 40 caracteres (max 40 bytes)</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function SetHeader(nVar1 As Integer, strVar2 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@SetHeader" & "|" & Str(nVar1) & "|" & strVar2	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Programar texto de cola</summary>
  ''' <remarks>Este comando almacena un l�nea de datos fijos que aparece en el encabezado o pie de p�gina de los comprobantes fiscales. Se permite hasta un m�ximo de 5 l�nea para el encabezado y para el pie de p�gina.</remarks>
  ''' <param name='nVar1'>N�mero de l�nea de datos fijos (nn)</param>
  ''' <param name='strVar2'>Texto Fiscal de hasta 40 caracteres (max 40 bytes)</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function SetTrailer(nVar1 As Integer, strVar2 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@SetTrailer" & "|" & Str(nVar1) & "|" & strVar2	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Abrir gaveta de dinero 1</summary>
  ''' <remarks>Este comando es utilizado para abrir la gaveta(s) de dinero conectada al puerto de la impresora fiscal.</remarks>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function OpenDrawer1() As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@OpenDrawer1"	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Abrir gaveta de dinero 2</summary>
  ''' <remarks>Este comando es utilizado para abrir la gaveta(s) de dinero conectada al puerto de la impresora fiscal.</remarks>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function OpenDrawer2() As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@OpenDrawer2"	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Obtener el Nro de serie de la impresora</summary>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function SerialRequest() As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@SerialRequest"	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Obtener el Nro de serie de la impresora</summary>
  ''' <param name='dblVar1'>Tasa Standard (.nnnn)</param>
  ''' <param name='dblVar2'>Tasa IVA 2 (.nnnn)</param>
  ''' <param name='dblVar3'>Tasa IVA 3 (.nnnn)</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function ProgramTaxes(dblVar1 As Double, dblVar2 As Double, dblVar3 As Double) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@ProgramTaxes" & "|" & Str(dblVar1) & "|" & Str(dblVar2) & "|" & Str(dblVar3)	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

 End Class
End Namespace

