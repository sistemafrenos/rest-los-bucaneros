' Este modulo contiene el código a disposicion por parte de IFDRIVERS
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
  ''' <remarks>Este comando es utilizado para evaluar el estado del controlador fiscal. También permite definir el comportamiento del equipo a las señales provenientes de los sensores de papel. Podrá ser ejecutado después y antes de cualquier otro comando.</remarks>
  ''' <param name='byVar1'>Tipo de información solicitada {NEABCDRFJSU}</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function StatusRequest(byVar1 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@StatusRequest" & "|" & byVar1	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Cierre de jornada fiscal</summary>
  ''' <remarks>Este comando imprime un reporte con los totales almacenados en la memoria de trabajo. Los montos pueden ser de los totales diarios (reporte Z) o de los totales parciales acumulados desde la emisión del
último reporte X. Al realizar un reporte Z los montos almacenados en la memoria de trabajo son llevados a la memoria fiscal.</remarks>
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
  ''' <remarks>Este comando permite generar un reporte que incluye los diferentes cierres diarios que han sido almacenados en la memoria fiscal durante el periodo de fechas seleccionado. Este comando usa tiempo extendido para su finalización. El reporte puede ser detallado o un resumen mensual.Adicionalmente, utilizando el calificador del comando con la opción ‘C’ la respuesta al comando contiene el rango de cierres diarios asociados al período de fechas. Este comando efectúa una verificación del contenido de la memoria fiscal. Su ejecución puede tardar varios minutos.</remarks>
  ''' <param name='strVar1'>Fecha de inicio de selección AAMMDD (max 6 bytes)</param>
  ''' <param name='strVar2'>Fecha de fin de selección AAMMDD (max 6 bytes)</param>
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
  ''' <remarks>Este comando permite generar un reporte conformado por una secuencia de cierres diarios. Este comando usa tiempo extendido para su finalización.</remarks>
  ''' <param name='nVar1'>Número de Z de inicio de selección (nnnn)</param>
  ''' <param name='nVar2'>Número de Z de fin de selección (nnnn)</param>
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
  ''' <remarks>Este comando es el primer paso para producir un comprobante fiscal. Se imprime el encabezado, el número, y la fecha y la hora de emisión del comprobante fiscal. Esta información se registra en la
memoria de trabajo.Se rechazará el comando si hay un comprobante fiscal abierto, si la memoria fiscal está llena, si hay un error en la memoria de trabajo, o si es necesario realizar un cierre de jornada (Reporte Z).
Este comando puede ser utilizado para generar comprobantes de devolución (notas de crédito), para esto se el calificador del comando (campo Nro 5) debe ser igual a 'D'. Si se está realizando una Nota de Crédito son permitidos todos los comandos que aplican para un comprobante
fiscal normal, excepto el comando ReturnRecharge.</remarks>
  ''' <param name='strVar1'>Razón social 1 (max 40 bytes)</param>
  ''' <param name='strVar2'>RIF del comprador (max 20 bytes)</param>
  ''' <param name='strVar3'>Nro de comprobante (en devolución) (max 20 bytes)</param>
  ''' <param name='strVar4'>Serial de la maquina fiscal que realizo el comprobante en devolución (Solo en nota de crédito) (max 20 bytes)</param>
  ''' <param name='strVar5'>Fecha del comprobante en devolución (Solo en nota de crédito) (max 6 bytes)</param>
  ''' <param name='strVar6'>Hora del comprobante en devolución (max 6 bytes)</param>
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
  ''' <remarks>Se rechazará el comando si no hay un comprobante fiscal abierto. El texto sólo puede ser 'texto fiscal', y debe tener una longitud máxima limitada de manera que no se pueda imprimir nada en las columnas que
normalmente están ocupadas por campos de montos de ítems de líneas. No se pueden imprimir más de 3 líneas de texto fiscal consecutivas. Si se efectúo previamente un cierre parcial de un comprobante fiscal
se permitirá ejecutar el comando 0x41. Se debe ejecutar de nuevo el comando CloseFiscalReceipt para finalizar el comprobante. Si el campo 2 es igual 'S' no se imprime el texto en la cinta de
auditoria.</remarks>
  ''' <param name='strVar1'>Texto Fiscal a Imprimir (max 26 bytes)</param>
  ''' <param name='byVar2'>Calificador de Impresión {SO}</param>
  ''' <returns> 0 si no hay error, != 0 si hay un error</returns>
  Public Function PrintFiscalText(strVar1 As String, byVar2 As String) As Integer
   Dim nError As Long
   Dim strBuff As String
   
   strBuff = "@PrintFiscalText" & "|" & strVar1 & "|" & byVar2	
   
   nError = IF_WRITE(strBuff)
   
   return(nError)
  End Function

  ''' <summary>Imprimir item</summary>
  ''' <remarks>No se aceptará el comando si no hay un comprobante fiscal abierto. Se rechazará si la acumulación de montos genera un desborde de totales. El texto se encuentra limitado a 'texto fiscal' (se pueden utilizar
los efectos de impresión, solo para la descripción del ítem).</remarks>
  ''' <param name='strVar1'>Descripción de hasta 20 caracteres (max 20 bytes)</param>
  ''' <param name='dblVar2'>Cantidad (nnnn.nnn)</param>
  ''' <param name='dblVar3'>Monto del ítem (nnnnnnnn.nn)</param>
  ''' <param name='dblVar4'>Tasa impositiva (.nnnn)</param>
  ''' <param name='byVar5'>Calificador de ítem de línea {Mm}</param>
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
  ''' <remarks>Este comando será rechazado si no hay un comprobante fiscal abierto. Este comando es útil para verificar que los montos acumulados en la impresora fiscal, a través del proceso de facturación,
concuerdan con los llevados por el software en el host. Luego de este comando se pueden emitir comandos de impresión de ítem adicionales.</remarks>
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
  ''' <remarks>Se rechazará este comando si no hay un comprobante fiscal abierto, si los montos acumulados generan un desbordamiento de total. Se usa este comando para imprimir información del total y del pago de la
transacción. Después de este comando, no se pueden emitir comandos de impresión de línea de ítem adicionales.</remarks>
  ''' <param name='strVar1'>Descripción de 20 caracteres (max 20 bytes)</param>
  ''' <param name='dblVar2'>Monto de pago (nnnnnn.nn)</param>
  ''' <param name='byVar3'>Calificador de comando {CTDP}</param>
  ''' <param name='dblVar4'>Tasa impositiva sobre la que aplica la promoción (.nnnn)</param>
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
  ''' <remarks>Se rechazará el comando si no hay un comprobante fiscal abierto, o si los montos acumulativos originan un desbordamiento del total. Este comando se usa para cerrar el comprobante fiscal, acumular totales en
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
encabezado y el número del documento. El comando será rechazado si hay un comprobante fiscal o un
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
  ''' <remarks>El comando será rechazado si no está abierto un documento no fiscal. Se restringirá el texto al conjunto
de caracteres definidos como “texto fiscal”.</remarks>
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
  ''' <remarks>El comando será rechazado si no está abierto un documento no fiscal. Este comando se utiliza para cerrar el documento no fiscal.
Si el calificador de comando es “E”, se termina el documento, se corta el papel y se imprimen las primeras líneas descriptivas del próximo comprobante fiscal. Después de cerrar el documento de esta
manera solo se podrá emitir un comprobante fiscal, en caso contrario se deberá cortar el papel.</remarks>
  ''' <param name='byVar1'>Calificador de comando. 'E' = indica cierre económico. Se imprime encabezado del próximo comprobante</param>
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
  ''' <remarks>Este comando se utiliza para cortar el papel de recibo. Los comprobantes fiscales, los documentos no fiscales y los reportes con cortados automáticamente al finalizar los comandos respectivos</remarks>
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
  ''' <param name='strVar3'>Fecha de emisión (max 20 bytes)</param>
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
  ''' <param name='strVar1'>Formato de Fecha AAMMDD (Año, Mes, Día) (max 6 bytes)</param>
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
  ''' <remarks>Este comando almacena un línea de datos fijos que aparece en el encabezado o pie de página de los comprobantes fiscales. Se permite hasta un máximo de 5 línea para el encabezado y para el pie de página.</remarks>
  ''' <param name='nVar1'>Número de línea de datos fijos (nn)</param>
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
  ''' <remarks>Este comando almacena un línea de datos fijos que aparece en el encabezado o pie de página de los comprobantes fiscales. Se permite hasta un máximo de 5 línea para el encabezado y para el pie de página.</remarks>
  ''' <param name='nVar1'>Número de línea de datos fijos (nn)</param>
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

