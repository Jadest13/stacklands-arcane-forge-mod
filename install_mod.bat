@echo off
setlocal enabledelayedexpansion

set "MOD=ArcaneForge"

REM === 경로 설정 ===
set "SRC=%cd%"
set "DEST=%AppData%\..\LocalLow\sokpop\Stacklands\Mods\%MOD%"
set "LIST=%SRC%\copylist.txt"

REM === 대상 폴더 생성 ===
mkdir "%DEST%" >nul 2>nul

REM == dll 파일 꺼내기
echo Copying %MOD%.dll to mod folder ...
robocopy "%SRC%\bin\Debug\netstandard2.1" "%DEST%" "%MOD%.dll" /R:0 /W:0 /COPY:DAT /NFL /NDL /NJH /NJS /NP >nul
set "RC=!ERRORLEVEL!"
if !RC! GEQ 8 (
  echo   robocopy error (rc=!RC!^) on file %MOD%.dll
)

if not exist "%LIST%" (
  echo ❌ copylist.txt not found at "%LIST%"
  echo    Create copylist.txt and list files/folders to include.
  pause
  exit /b 1
)

echo Copying ONLY items listed in copylist.txt ...
echo FROM: "%SRC%"
echo TO  : "%DEST%"
echo.

for /f "usebackq tokens=* delims=" %%I in ("%LIST%") do (
  set "ITEM=%%I"

  REM --- 앞뒤 공백 제거용 임시 변환 (필요시)
  REM set "ITEM=!ITEM:~0!"

  REM --- 빈 줄/주석 건너뛰기 ---
  if not defined ITEM (
    REM skip blank
  ) else (
    if "!ITEM:~0,1!"=="#" (
      REM skip comment
    ) else (
      set "LASTCHAR=!ITEM:~-1!"

      REM --- 폴더일 때 (끝이 \ 로 끝남) ---
      if "!LASTCHAR!"=="\" (
        if exist "%SRC%\!ITEM!" (
          REM 끝의 \ 제거해서 정규화
          set "ITEMN=!ITEM:~0,-1!"
          echo [DIR ] !ITEMN!

          robocopy "%SRC%\!ITEMN!" "%DEST%\!ITEMN!" /E /R:0 /W:0 /MT:16 /COPY:DAT /NFL /NDL /NJH /NJS /NP >nul
          set "RC=!ERRORLEVEL!"
          if !RC! GEQ 8 (
            echo   robocopy error (rc=!RC!^) on folder !ITEMN!
          )
        ) else (
          echo [SKIP] Folder not found: !ITEM!
        )
      ) else (
        REM --- 파일일 때 ---
        if exist "%SRC%\!ITEM!" (
          echo [FILE] !ITEM!
          robocopy "%SRC%" "%DEST%" "!ITEM!" /R:0 /W:0 /COPY:DAT /NFL /NDL /NJH /NJS /NP >nul
          set "RC=!ERRORLEVEL!"
          if !RC! GEQ 8 (
            echo   robocopy error (rc=!RC!^) on file !ITEM!
          )
        ) else (
          echo [SKIP] File not found: !ITEM!
        )
      )
    )
  )
)

echo.
echo Selected items copied successfully!
echo (Only items listed in copylist.txt were included.)